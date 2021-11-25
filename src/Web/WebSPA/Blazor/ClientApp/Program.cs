var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<ICatalogApiClient, CatalogApiClient>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://0.0.0.0:8010") });

await builder.Build().RunAsync();
