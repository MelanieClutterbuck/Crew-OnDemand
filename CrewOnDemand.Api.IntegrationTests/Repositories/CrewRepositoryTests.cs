using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CrewOnDemand.Api.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrewOnDemand.Api.IntegrationTests.Repositories
{
    public class CrewRepositoryTests : IClassFixture<CrewRepositoryTests>
    {
        private readonly IConfiguration _config;

        public CrewRepositoryTests()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.test.json", false, false).Build();
        }

        [Fact]
        public async Task Get_Returns_AllCrewMembers()
        {
            var mockLogger = new Mock<ILogger<JsonCrewRepository>>();

            var repo = new JsonCrewRepository(_config, mockLogger.Object);

            var crewMembers = await repo.Get();

            Assert.Equal(8, crewMembers.Count);
            
            Assert.Collection(crewMembers,
                item =>
                {
                    item.Id.Equals(1);
                    item.Name.Equals("Andy");
                    item.Base.Equals("Munich");
                    item.WorkDays.Equals(new string[] {"Monday", "Tuesday", "Thursday", "Saturday"});

                },
                item =>
                {
                    item.Id.Equals(2);
                    item.Name.Equals("Betty");
                    item.Base.Equals("Munich");
                    item.WorkDays.Equals(new string[] { "Monday", "Tuesday", "Wednesday", "Friday" });

                },
                item =>
                {
                    item.Id.Equals(3);
                    item.Name.Equals("Callum");
                    item.Base.Equals("Munich");
                    item.WorkDays.Equals(new string[] { "Wednesday", "Thursday", "Saturday", "Sunday" });

                },
                item =>
                {
                    item.Id.Equals(4);
                    item.Name.Equals("Daphne");
                    item.Base.Equals("Munich");
                    item.WorkDays.Equals(new string[] { "Friday", "Saturday", "Sunday" });

                },
                item =>
                {
                    item.Id.Equals(5);
                    item.Name.Equals("Elvis");
                    item.Base.Equals("Berlin");
                    item.WorkDays.Equals(new string[] { "Monday", "Tuesday", "Thursday", "Saturday" });

                },
                item =>
                {
                    item.Id.Equals(6);
                    item.Name.Equals("Freida");
                    item.Base.Equals("Berlin");
                    item.WorkDays.Equals(new string[] { "Monday", "Tuesday", "Wednesday", "Friday" });

                },
                item =>
                {
                    item.Id.Equals(7);
                    item.Name.Equals("Greg");
                    item.Base.Equals("Berlin");
                    item.WorkDays.Equals(new string[] { "Wednesday", "Thursday", "Saturday", "Sunday" });

                },
                item =>
                {
                    item.Id.Equals(8);
                    item.Name.Equals("Hermione");
                    item.Base.Equals("Berlin");
                    item.WorkDays.Equals(new string[] { "Friday", "Saturday", "Sunday" });

                });
        }
    }
}
