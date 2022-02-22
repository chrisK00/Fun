using MemorySignal.Admin;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]) };
var key = "Token";
httpClient.DefaultRequestHeaders.Add(key, builder.Configuration[key]);
builder.Services.AddScoped<HttpClient>(_ => httpClient);

await builder.Build().RunAsync();
