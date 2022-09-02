using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Monxterz.SquadRpg.BlazorClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseUrl = builder.Configuration["BaseUrl"] ?? builder.HostEnvironment.BaseAddress;
builder.Services.AddMonxterzSquad(baseUrl);
builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
