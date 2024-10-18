using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using flashcardApp.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IDecksService, DecksService>();

// builder.Services.AddScoped<DemoLoggingHandler>();
// builder.Services.AddHttpClient("flashcardApp.Api", httpClient => httpClient.BaseAddress = new Uri(builder.Configuration["ApiUrl"]))
//     .AddHttpMessageHandler<DemoLoggingHandler>();
// builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("flashcardApp.Api"));

await builder.Build().RunAsync();
