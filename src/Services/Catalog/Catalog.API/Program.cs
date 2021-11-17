using System.Reflection;
using Catalog.Application.Behaviours;
using Catalog.Application.Contracts.Persistence;
using Catalog.Application.Features.Products.Queries.GetProductsList;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Add DI Catalog Service

//TODO: Infrastructure.Register
builder.Services.AddDbContext<CatalogContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogConnectionString"), b => b.MigrationsAssembly("Catalog.API")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//TODO: Application.Register
builder.Services.AddAutoMapper(typeof(GetProductsListQuery).GetTypeInfo().Assembly);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(GetProductsListQuery).GetTypeInfo().Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));


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
