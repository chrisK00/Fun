using MemorySignal.Core.Data;
using MemorySignal.Server.Hubs;
using MemorySignal.Shared.Interfaces;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .Enrich.FromLogContext()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opt => opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddCoreServices(builder.Configuration, builder.Environment.IsProduction());

var app = builder.Build();
Seed(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseBlazorFrameworkFiles();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapHub<MemoryGameHub>(IMemoryGameHub.Uri);
app.MapFallbackToFile("index.html");

app.Run();

static void Seed(IServiceProvider sp)
{
    using var scope = sp.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    if (context.CardCollections.Any()) return;

    var smallAnimalCards = new List<Card>()
    {
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-gd8d8e4a91_640_sctbst.jpg", "https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-gd8d8e4a91_640_sctbst.jpg"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-g4e9c2c7b0_640_prkfyy.jpg", "https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/bird-g4e9c2c7b0_640_prkfyy.jpg"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/black-and-white-warbler-gb189f6056_640_hrhl6f.jpg", "https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/black-and-white-warbler-gb189f6056_640_hrhl6f.jpg"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/squirrel-gd5b52f387_640_oixdk0.jpg", "https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455062/Cards/squirrel-gd5b52f387_640_oixdk0.jpg"),
    };

    var bigAnimalCards = new List<Card>()
    {
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gec88cbf11_640_ww0aib.jpg","https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gec88cbf11_640_ww0aib.jpg"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gdf5cd6fc2_640_jgcci2.jpg","https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/dog-gdf5cd6fc2_640_jgcci2.jpg"),
        new Card("https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/labrador-retriever-g30db09fa2_640_kvcgx0.jpg","https://res.cloudinary.com/dbb9v8ne8/image/upload/v1645455145/Cards/labrador-retriever-g30db09fa2_640_kvcgx0.jpg"),
    };

    var smallAnimalsCardCollection = new CardCollection("Small Animals", smallAnimalCards);
    var bigAnimalsCardCollection = new CardCollection("Big Animals", bigAnimalCards);
    context.AddRange(smallAnimalsCardCollection, bigAnimalsCardCollection);
    context.SaveChanges();
}
