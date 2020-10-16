using System;
using System.Threading.Tasks;
using CrewOnDemand.Api.Bus;
using CrewOnDemand.Api.Data;
using CrewOnDemand.Api.Services;
using CrewOnDemand.Api.UnitTests.TestData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CrewOnDemand.Api.UnitTests.CrewServices
{
    public class CrewBookingServiceTests
    {
        private readonly Mock<ILogger<CrewBookingService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<ICrewBookingRepository> _mockCrewBookingRepo;
        private readonly Mock<IMessageBus> _mockMessageBus;

        public CrewBookingServiceTests()
        {
            _mockMessageBus = new Mock<IMessageBus>();
            _mockLogger = new Mock<ILogger<CrewBookingService>>();
            _mockConfig = new Mock<IConfiguration>();

            _mockCrewBookingRepo = new Mock<ICrewBookingRepository>();
        }

        [Fact]
        public async void SuccessfullyAddingToTheBookingRepo_SendsMessageOnBus()
        {
            _mockCrewBookingRepo.Setup(m =>
                m.Add(It.IsAny<CrewBooking>())).Returns(Task.FromResult(true));

            var crewBookingService = new CrewBookingService(_mockConfig.Object, _mockLogger.Object,
                _mockCrewBookingRepo.Object, _mockMessageBus.Object);

            var pilot = await crewBookingService.BookPilot(new CrewMember(), new CrewBooking());

            _mockCrewBookingRepo.Verify(m => m.Add(It.IsAny<CrewBooking>()), Times.Exactly(1));

            _mockMessageBus.Verify(m => m.SendAsync(It.IsAny<IMessage>()), Times.Exactly(1));
        }
    }
}
