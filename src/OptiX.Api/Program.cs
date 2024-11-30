using Microsoft.EntityFrameworkCore;
using OptiX.Application.Asset;
using OptiX.Application.Binance;
using OptiX.Application.MarketData;
using Optix.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ITicksService, TicksService>();

builder.Services.AddHostedService<BinanceMarketDataLoader>();

var app = builder.Build();

var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<AppDbContext>()?.Database;
await database?.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.Run();