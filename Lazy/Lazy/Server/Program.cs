using Lazy.Data;
using Lazy.EF;
using Lazy.Data.Entities;
using Lazy.EF.Repository;
using Lazy.Model;
using Lazy.Server.Mappers;
using Lazy.Util.EntityModelMapper;
using Lazy.Client.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        //.WriteTo.Console()
        //.WriteTo.File("col.log")
        .CreateLogger();

logger.Information("Hello, world!");

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDb(builder.Configuration.GetConnectionString("LazyConStr"));

builder.Services.AddScoped(typeof(UserSettingsService));

// mappers
builder.Services
    .AddSingleton(
        typeof(IEntityModelMapper<EmailTemplate, EmailTemplateModel>),
        typeof(EmailTemplateMapper))
    .AddSingleton(
        typeof(IEntityModelMapper<PagedRepositoryResult<EmailTemplate>, PagedModelResult<EmailTemplateModel>>),
        typeof(EmailTemplatePagedResultMapper));



// repositories
builder.Services.AddScoped<IRepository<EmailTemplate, int>, EmailTemplateRepository>();


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