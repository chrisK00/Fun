using MemorySignal.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//string apiUrl = builder.HostEnvironment.IsProduction() ? builder.Configuration["ApiUrl"] : builder.Configuration["LocalClient"]; "LocalClient": "http://localhost:5190/"
string apiUrl = builder.Configuration["ApiUrl"];

builder.Services.Configure<ApiOptions>(opt => opt.Url = apiUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });

await builder.Build().RunAsync();
