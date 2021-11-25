using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;
using Microsoft.IdentityModel.Tokens;

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

var authenticationProviderKey = "IdentityApiKey";

// NUGET - Microsoft.AspNetCore.Authentication.JwtBearer
builder.Services.AddAuthentication()
    .AddJwtBearer(authenticationProviderKey, x =>
    {
        x.Authority = "https://localhost:8005"; // IDENTITY SERVER URL
                                                //x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });


builder.Services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors("AllowAnyOrigins");
await app.UseOcelot();


app.Run();
