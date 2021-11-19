using System.Security.AccessControl;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using System.Runtime.InteropServices;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// Grpc Configuration
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
(o => o.Address = new Uri(builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl")));

var urlGrpc = builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl");
var channel = GrpcChannel.ForAddress(urlGrpc);
builder.Services.AddScoped<DiscountGrpcService>();


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
