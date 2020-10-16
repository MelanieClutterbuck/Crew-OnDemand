using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrewOnDemand.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CrewOnDemand.Api.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {

        /// <summary>
        /// override enables us to configure DI in the services collection, once per web app factory.
        /// the web app factory can be used by multiple tests.
        /// </summary>
        /// <param name="builder"></param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder.ConfigureTestServices(services =>
            {
                           
            });
        }
    }
}
