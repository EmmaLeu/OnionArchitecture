using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        ///<summary>
        ///This method gets called by the runtime. Use this method to add services to the container.
        ///</summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(a =>
            {
                a.IncludeXmlComments(string.Format(@"{0}\OnionArchitecture.xml", AppDomain.CurrentDomain.BaseDirectory));
                a.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OnionArchitecture"
                });
            });

            services.AddApplication();
        }

        ///<summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionArchitecture");
            });
        }
    }
}
