using Microsoft.EntityFrameworkCore;
using OptiX.Application.Account;
using OptiX.Application.Asset;
using OptiX.Application.Binance;
using OptiX.Application.MarketData;
using OptiX.Application.SignalR;
using Optix.Infrastructure.Database;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Frontend",
        policy  =>
        {
            policy.WithOrigins("http://localhost:8080", "https://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<ITicksService, TicksService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddHostedService<BinanceMarketDataLoader>();

var app = builder.Build();

app.UseCors("Frontend");

var scope = app.Services.CreateScope();
var database = scope.ServiceProvider.GetService<AppDbContext>()?.Database;
await database?.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.UseHttpsRedirection();

app.MapHub<MarketDataHub>("/market-data");

app.Run();