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
    public class CrewBookingRepositoryTests : IClassFixture<CrewBookingRepositoryTests>
    {
        private readonly IConfiguration _config;
        
        public CrewBookingRepositoryTests()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.test.json", false, false).Build();
        }

        [Fact]
        public async Task Get_Returns_AllCrewMembersBookings()
        {
            var mockLogger = new Mock<ILogger<JsonCrewBookingRepository>>();

            var repo = new JsonCrewBookingRepository(_config, mockLogger.Object);

            var bookings = await repo.Get();

            Assert.Equal(4, bookings.Count);
            Assert.Equal(2, bookings.Count(b => b.CrewId == 2));
            Assert.Collection(bookings,
                item =>
                {
                    item.CrewId.Equals(1);
                    item.Duration.Equals(2);
                },
                item =>
                {
                    item.CrewId.Equals(2);
                    item.Duration.Equals(4);
                },
                item =>
                {
                    item.CrewId.Equals(2);
                    item.Duration.Equals(4);
                },
                item =>
                {
                    item.CrewId.Equals(5);
                    item.Duration.Equals(2);
                });
        }
    }
}
