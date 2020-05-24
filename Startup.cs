using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW01.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HW01
{
    public class Startup
    {
        public static readonly ILoggerFactory MyLoggerFactory =
        LoggerFactory.Create(
            builder => { builder.AddConsole(); }
        );
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DI
            services.AddDbContext<ContosouniversityContext>(options =>
                options.UseLoggerFactory(MyLoggerFactory)
                        .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) //在DI中設定不要開啟dbContext的快取功能
                );


            services.AddControllers()
                    .AddNewtonsoftJson();

            // Register the Swagger services
            services.AddSwaggerDocument();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
