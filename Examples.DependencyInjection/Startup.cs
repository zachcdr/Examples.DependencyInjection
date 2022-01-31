using Examples.DependencyInjection.Interfaces;
using Examples.DependencyInjection.Models;
using Examples.DependencyInjection.Models.Enums;
using Examples.DependencyInjection.Repositories;
using Examples.DependencyInjection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Examples.DependencyInjection
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
            services.AddControllers();

            services.AddLogging();

            // build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            services.Configure<ApplicationSettings>(configuration.GetSection("AppSettings"));

            // Service and Repository dependency injection
            services.AddTransient<IPizzaRepo, PizzaRepo>();
            services.AddTransient<IPizzaService, PizzaService>();

            // I wanted to show an example where I have an interface inherited my multiple services (this could also be done by declaring a public delegate)
            services.AddTransient<FlatRateServiceChargeService>();
            services.AddTransient<PercentageRateServiceChargeService>();
            services.AddTransient<Func<IServiceRateType, IServiceCharge>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case IServiceRateType.Flat:
                        return serviceProvider.GetService<FlatRateServiceChargeService>();
                    case IServiceRateType.Percentage:
                        return serviceProvider.GetService<PercentageRateServiceChargeService>();
                    default:
                        throw new KeyNotFoundException();
                }
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
