using MemorySignal.Client;
using MemorySignal.Client.Options;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string apiUrl = builder.HostEnvironment.IsProduction() ? builder.Configuration["ApiUrl"] : builder.HostEnvironment.BaseAddress;

builder.Services.Configure<ApiOptions>(opt => opt.Url = apiUrl);
builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(apiUrl) });

await builder.Build().RunAsync();
