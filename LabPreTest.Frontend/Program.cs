using LabPreTest.Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LabPreTest.Frontend.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7095/") });
builder.Services.AddScoped<IRepository, Repository>();

await builder.Build().RunAsync();
                
builder.Services.AddScoped<IRepository, Repository>();