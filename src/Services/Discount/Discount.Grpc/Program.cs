//https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1#call-a-grpc-service-with-an-untrustedinvalid-certificate

using Discount.Grpc.Data;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<DiscountContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DiscountConnectionString"), b => b.MigrationsAssembly("Discount.Grpc")));

//MacOs Http/2 Support
//builder.WebHost.ConfigureKestrel(opt => opt.ListenLocalhost(8003, o => o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2));

var app = builder.Build();

// Configure the HTTP request pipeline.
InitializeDatabase(app);
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

void InitializeDatabase(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
    {
        serviceScope.ServiceProvider.GetRequiredService<DiscountContext>().Database.Migrate();
        //serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
    }
}