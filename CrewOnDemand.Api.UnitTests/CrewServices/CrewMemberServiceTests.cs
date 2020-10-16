using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using CrewOnDemand.Api;
using CrewOnDemand.Api.Data;
using CrewOnDemand.Api.Services;
using CrewOnDemand.Api.UnitTests.TestData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace CrewOnDemand.Api.UnitTests.CrewServices
{
    public class CrewMemberServiceTests
    {
        private readonly Mock<ILogger<CrewMemberService>> _mockLogger;
        private readonly Mock<ILogger<FairPilotFinder>> _mockPilotFinderLogger;
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<ICrewBookingRepository> _mockCrewBookingRepo;
        private readonly Mock<ICrewRepository> _mockCrewRepo;

        public CrewMemberServiceTests()
        {
            this._mockLogger = new Mock<ILogger<CrewMemberService>>();
            this._mockPilotFinderLogger = new Mock<ILogger<FairPilotFinder>>();
            this._mockConfig = new Mock<IConfiguration>();
            this._mockCrewBookingRepo = new Mock<ICrewBookingRepository>();
            this._mockCrewRepo = new Mock<ICrewRepository>();
            //this._mockPilotFinder = new Mock<IPilotFinder>();
        }

        [Fact]
        public async void IsAvailableFor_Returns_ValidPilotWithNoBookings()
        {
            _mockCrewBookingRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewBookingsStub.GetCrewBookingsList()));

            _mockCrewRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewStub.GetCrewMembersList()));

            var crewMemberService = new CrewMemberService(_mockConfig.Object, _mockLogger.Object, _mockCrewBookingRepo.Object, 
                _mockCrewRepo.Object, new FairPilotFinder(_mockPilotFinderLogger.Object));

            var departing = new DateTime(2020, 5, 1, 9,0,0);
            var fromBase = "Munich";
            var flightDay = departing.DayOfWeek.ToString();
            var pilot = await crewMemberService.PilotAvailableFor(departing, fromBase);

            _mockCrewRepo.Verify(m => m.Get(), Times.Exactly(1));
            _mockCrewBookingRepo.Verify(m => m.Get(), Times.Exactly(1));

            Assert.Equal(4, pilot.Id);
            Assert.Equal("Daphne", pilot.Name);
            Assert.Contains(flightDay, pilot.WorkDays);
            Assert.Equal("Munich", pilot.Base);
        }

        [Fact]
        public async void IsAvailableFor_Returns_ValidPilotWithBookingsOnOtherDays()
        {
            _mockCrewBookingRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewBookingsStub.GetBusyCrewBookingsList()));

            _mockCrewRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewStub.GetCrewMembersList()));

            var crewMemberService = new CrewMemberService(_mockConfig.Object, _mockLogger.Object, _mockCrewBookingRepo.Object,
                _mockCrewRepo.Object, new FairPilotFinder(_mockPilotFinderLogger.Object));

            var departing = new DateTime(2020, 5, 1, 9, 0, 0);
            var fromBase = "Munich";
            var flightDay = departing.DayOfWeek.ToString();
            var pilot = await crewMemberService.PilotAvailableFor(departing, fromBase);

            _mockCrewRepo.Verify(m => m.Get(), Times.Exactly(1));
            _mockCrewBookingRepo.Verify(m => m.Get(), Times.Exactly(1));

            Assert.Contains(flightDay, pilot.WorkDays);
            Assert.Equal("Munich", pilot.Base);
        }

        [Fact]
        public async void IsAvailableFor_Returns_ValidPilotWithBookingsOnTheSameDay()
        {
            _mockCrewBookingRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewBookingsStub.GetBusySameDayCrewBookingsList()));

            _mockCrewRepo.Setup(r => r.Get())
                .Returns(Task.FromResult(CrewStub.GetBusySameDayCrewMembersList()));

            var crewMemberService = new CrewMemberService(_mockConfig.Object, _mockLogger.Object, _mockCrewBookingRepo.Object,
                _mockCrewRepo.Object, new FairPilotFinder(_mockPilotFinderLogger.Object));

            var departing = new DateTime(2020, 5, 2, 9, 0, 0);
            var fromBase = "Berlin";
            var flightDay = departing.DayOfWeek.ToString();
            var pilot = await crewMemberService.PilotAvailableFor(departing, fromBase);

            _mockCrewRepo.Verify(m => m.Get(), Times.Exactly(1));
            _mockCrewBookingRepo.Verify(m => m.Get(), Times.Exactly(1));

            Assert.Contains(flightDay, pilot.WorkDays);
            Assert.Equal("Berlin", pilot.Base);
        }
    }
}
