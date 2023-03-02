using Lazy.Client;
using Lazy.Client.Services;
using Lazy.Model;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<UserSettingsService>();

// https://stackoverflow.com/questions/59280153/dependency-injection-httpclient-or-httpclientfactory
// builder.Services.AddHttpClient<DataService<EmailTemplateModel>, EmailTemplateDataService>(); 
// builder.Services.AddHttpClient<PagedDataService<EmailTemplateModel>, EmailTemplateDataService>();

builder.Services
    .AddScoped<DataService<EmailTemplateModel>, EmailTemplateDataService>()
    .AddScoped<PagedDataService<EmailTemplateModel>, EmailTemplateDataService>();
    


await builder.Build().RunAsync();