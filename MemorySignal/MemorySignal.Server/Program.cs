using MemorySignal.Server.Extensions;
using MemorySignal.Server.Filters;
using MemorySignal.Server.Hubs;
using MemorySignal.Shared.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger.Configure();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opt => opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

builder.Services.AddScoped<TokenFilter>();

builder.Services.AddCoreServices(builder.Configuration, builder.Environment.IsProduction());

var app = builder.Build();
app.Services.SeedDatabase();

app.UseForwardedHeaders();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.UseCors(builder =>
    {
        builder.WithOrigins(app.Configuration["ClientUrl"], app.Configuration["AdminUrl"])
               .AllowAnyHeader()
               .AllowAnyMethod();
    });

    app.MapFallback(async req => await req.Response.WriteAsJsonAsync("This link appears to be broken or the site is down for maintenance"));
}
else
{
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

app.MapControllers();
app.MapHub<MemoryGameHub>($"/{IMemoryGameHub.Uri}");

//app.Run("http://localhost:5190");
app.Run();
