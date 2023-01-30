using Lazy.DB;
using Lazy.DB.Entities;
using Lazy.DB.EntityModelMapper;
using Lazy.Server.Mappers;
using Lazy.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDb(builder.Configuration.GetConnectionString("LazyConStr"));

AddClassMapings(builder.Services);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();



void AddClassMapings(IServiceCollection services)
{
    services.AddSingleton(typeof(IEntityModelMapper<EmailTemplate, EmailTemplateModel>), new EmailTemplateMapper());
}
