using BlazorCRUD.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;


///Enlaces para el login
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorCRUD.Client.Extensiones;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


//Servicios para el login
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();

//Console.WriteLine("RootPath" + builder.HostEnvironment);


