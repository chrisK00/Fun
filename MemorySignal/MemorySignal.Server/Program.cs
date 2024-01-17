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
Log.Logger.Warning("Hello");
Log.Logger.Warning(builder.Configuration["ClientUrl"]);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opt => opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);
builder.Services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);

// TODO client should use this to contact open endpoints. alt just use cors lol
// TODO create/update operations should use config.json username and pass
builder.Services.AddScoped<TokenFilter>();

builder.Services.AddCoreServices(builder.Configuration, builder.Environment.IsProduction());

var app = builder.Build();
app.Services.SeedDatabase();

app.UseForwardedHeaders();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsProduction())
{
    // TODO: Add global exception handler
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder =>
{
    builder.WithOrigins("http://209.38.252.145/", "http://209.38.252.145:5020/", "http://209.38.252.145", "http://209.38.252.145:5020", "http://localhost:5020/", "http://localhost:5020")
           .AllowAnyHeader()
           .AllowAnyMethod();
});
//if (app.Environment.IsProduction())
//{
//    // todo client url nginx https
//    app.UseCors(builder => builder.WithOrigins(app.Configuration["ClientUrl"], app.Configuration["AdminUrl"]).AllowAnyHeader().AllowAnyMethod());
//    app.MapFallback(async req => await req.Response.WriteAsJsonAsync("This link appears to be broken or the site is down for maintenance"));
//}
//else
//{
//    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//}

app.MapControllers();
app.MapHub<MemoryGameHub>("/" + IMemoryGameHub.Uri);

app.Run();
