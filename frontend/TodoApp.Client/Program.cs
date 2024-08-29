using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TodoApp.Client;
using TodoApp.Client.PageModel;
using TodoApp.Client.WebApiRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddScoped<HomePageModel>();

#region DI

builder.Services.AddScoped<ITodoWebApi, TodoWebApi>();

#endregion DI

await builder.Build().RunAsync();