using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                });

builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();
            }));

builder.Services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors("AllowAnyOrigins");
await app.UseOcelot();


app.Run();
