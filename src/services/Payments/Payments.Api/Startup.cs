using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payments.Service.Queries.Plan.Interfaces;
using Payments.Service.Queries.Plan.Services;
using Peyment.Persistence.Database;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Cors
            services.AddCors();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ApplicationDbContext>(
              cnn => cnn.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection"),
                  x => x.MigrationsHistoryTable("_EFMigrationsHistory", "Makaya")
              )
          );
            services.AddSingleton(c => Configuration);
            // Add MVC services to the services container.
            services.AddMvc();
            // Event handlers
            services.AddMediatR(Assembly.Load("Payment.Service.EventHandlers")); 
            // Query services
            services.AddTransient<IPlanQueryService, PlanQueryService>();
            services.AddControllers();

            StripeConfiguration.ApiKey = Configuration.GetValue<string>("Secret_Key_Strinpe");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Cors
            app.UseCors(opt => {
                opt.WithOrigins("*");
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
