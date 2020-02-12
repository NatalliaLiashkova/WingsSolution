using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Globalization;
using System.IO;
using WingsAPI.CustomException;
using WingsAPI.Logging;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOnServices;
using WingsOnServices.Interfaces;

namespace WingsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var appBasePath = System.IO.Directory.GetCurrentDirectory();
            GlobalDiagnosticsContext.Set("appbasepath", appBasePath);
            LogManager.LoadConfiguration(Path.Combine(appBasePath,"NLog.config"));
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Culture = new CultureInfo("nl-NL");
                });

            services.AddSingleton<ILog, LogNLog>();
            services.AddSingleton<IRepository<Booking>, BookingRepository>();
            services.AddSingleton<IRepository<Flight>, FlightRepository>();
            services.AddSingleton<IRepository<Person>, PersonRepository>();

            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IPassengerService, PassengersService>();
            services.AddTransient<IBookingService, BookingService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(logger);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
