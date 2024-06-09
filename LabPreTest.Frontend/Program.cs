using LabPreTest.Frontend;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LabPreTest.Frontend.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using LabPreTest.Frontend.AuthenticationProviders;
using LabPreTest.Frontend.Services;
using Blazored.Modal;


//var urlBackend = "https://labpretestbackend.azurewebsites.net/";
var urlBackend = "https://localhost:7095/";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(urlBackend) });
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSweetAlert2();
builder.Services.AddBlazoredModal();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationProviderJWT>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderJWT>(x =>
        x.GetRequiredService<AuthenticationProviderJWT>());
builder.Services.AddScoped<ILoginService, AuthenticationProviderJWT>(x => 
        x.GetRequiredService<AuthenticationProviderJWT>());

await builder.Build().RunAsync();