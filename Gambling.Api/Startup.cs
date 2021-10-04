using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Gambling.Common;
using Gambling.Data;
using Gambling.Data.Contracts;
using Gambling.Data.Tools;
using Gambling.Data.Tools.Enums;
using Gambling.Entities.User;
using Gambling.WebFramework.Configuration;
using Gambling.WebFramework.Configuration.DI;
using Gambling.WebFramework.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Api
{
    public class Startup
    {
        private readonly SiteSettings _siteSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));


            services.AddDbContext<ApiContext>(options =>
            {
                options.UseInMemoryDatabase("StakeDb");
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>(sp =>
            {
                Options options =
                    new Options
                    {
                        Provider =
                            (Provider)
                            System.Convert.ToInt32(Configuration.GetSection(key: "databaseProvider").Value),
                    };

                return new UnitOfWork(options: options);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo
                    {
                        Title = "Gambling Api", 
                        Version = "v1",
                        Description = "Through this api you can access Gambling Game",
                        Contact = new OpenApiContact()
                        {
                            Name = "Aras Rasti",
                            Email = "aras.rasti@gmail.com",
                            Url =new Uri("https://github.com/aras-rasti/Gambling")
                        }
                    });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddCustomApplicationServices();
            services.AddCustomAuthentication(_siteSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
   

            app.UseCustomExceptionHandler();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gambling Api v1"));
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
