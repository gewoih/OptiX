using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using MyApplication.Components;
using OptiX.Application.Asset;
using OptiX.Application.Binance;
using OptiX.Application.MarketData;
using Optix.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ITicksService, TicksService>();

builder.Services.AddHostedService<BinanceMarketDataLoader>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<AppDbContext>()?.Database;
await database?.MigrateAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
