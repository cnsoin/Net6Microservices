using MassTransit;
using Ordering.API.EventBusConsumer;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;
using static Common.Shared.Constants;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Add DI Ordering Service

//TODO: Infrastructure.Register
builder.Services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("OrderingConnectionString"), b => b.MigrationsAssembly("Ordering.API")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.Configure<EmailSettings>(c => builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

//TODO: Application.Register
builder.Services.AddAutoMapper(typeof(GetOrdersListQuery).GetTypeInfo().Assembly);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(GetOrdersListQuery).GetTypeInfo().Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


//TODO: MassTransit-RabbitMQ Configuration
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketCheckoutConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration.GetValue<string>("EventBusSettings:HostAddress"));

        cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
            {
                c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
            });
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddScoped<BasketCheckoutConsumer>();
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddAutoMapper(typeof(BasketCheckoutConsumer).GetTypeInfo().Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
