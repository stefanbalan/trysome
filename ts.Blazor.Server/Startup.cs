using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ts.Database;

namespace ts.Blazor.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            //Configurations
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            //services.AddDbContext<PolicyCenterCentralDevContext>((serviceProvider, options) =>
            //{
            //    var settings = (IOptions<AppSettings>)serviceProvider.GetService(typeof(IOptions<AppSettings>));
            //    options.UseSqlServer(settings.Value.ConnectionString);
            //});

            //services.AddScoped<RepositoryBase<Product, DataContext>, ProductRepository>();

            services.AddMvc()
                    .AddNewtonsoftJson(op =>
                    {
                        op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //todo add models and remove (this is needed because we use EF classes)
                        op.SerializerSettings.Formatting = Formatting.Indented; //todo add conditionally only for Develepment environment
                    });

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet
                    });
            });

            //todo add swagger
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
