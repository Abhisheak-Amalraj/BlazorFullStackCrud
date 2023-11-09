global using BlazorFullStackCrud.Client.Services.UserService;
global using BlazorFullStackCrud.Client.Services.WaterIntakeService;
global using BlazorFullStackCrud.Shared;
using BlazorFullStackCrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWaterIntakeService, WaterIntakeService>();

await builder.Build().RunAsync();
