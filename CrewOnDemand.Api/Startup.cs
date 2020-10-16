using CrewOnDemand.Api.Bus;
using CrewOnDemand.Api.Data;
using CrewOnDemand.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api
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
            services.AddHealthChecks();
            services.AddLogging();

            services.AddSingleton<ICrewRepository, JsonCrewRepository>();
            services.AddSingleton<ICrewBookingRepository, JsonCrewBookingRepository>();
            services.AddScoped<IPilotFinder, FairPilotFinder>();
            services.AddSingleton<IMessageBus, MessageBus>(); 
            services.AddScoped<ICrewMemberService, CrewMemberService>();
            services.AddScoped<ICrewBookingService, CrewBookingService>();
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
                endpoints.MapHealthChecks("/healthcheck");
                endpoints.MapControllers();
            });
        }
    }
}
