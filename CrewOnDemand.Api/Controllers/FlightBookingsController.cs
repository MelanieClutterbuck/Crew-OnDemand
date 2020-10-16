using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CrewOnDemand.Api.Models;
using CrewOnDemand.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightBookingsController : ControllerBase
    {
        private readonly ICrewMemberService _crewMemberService;
        private readonly ICrewBookingService _crewBookingService;
        private readonly ILogger<FlightBookingsController> _logger;
        private readonly LinkGenerator _linkGenerator;

        public FlightBookingsController(ICrewMemberService crewMemberService, ICrewBookingService crewBookingService,
            ILogger<FlightBookingsController> logger, LinkGenerator linkGenerator)
        {
            _crewMemberService = crewMemberService;
            _crewBookingService = crewBookingService;
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingOutputModel>> Get([Required] int id)
        {
            _logger.LogInformation($"Getting booking with id: {id}");
            // this endpoint is only needed by the location link generator at the moment, after a POST
            return new ActionResult<BookingOutputModel>(new BookingOutputModel()
            {
                Id = 20298,
                PilotName = "Betty",
                StartLocation = "Munich",
                EndLocation = "Berlin",
                Departing = DateTime.Now.AddHours(4),
                Returning = DateTime.Now.AddHours(6),
                Duration = 1,
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(BookingInputModel model)
        {
            try
            {
                _logger.LogInformation($"Posting book request for Departing {model.Departing}, Returning {model.Returning}, StartLocation {model.StartLocation}," +
                                       $"EndLocation {model.EndLocation}, Duration {model.Duration}");

                if (await _crewBookingService.GetBooking(model.Id) != null)
                {
                    return Conflict("Message already exists.");
                }

                var location = _linkGenerator.GetPathByAction("Get", "FlightBookings", new { id = model.Id });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not generate link.");
                }

                var booking = model.ToBooking();

                var crewMember = await _crewMemberService.PilotAvailableFor(booking.Departing, booking.StartLocation);
                
                if (await _crewBookingService.BookPilot(crewMember, booking))
                {
                    _logger.LogInformation($"Completed posting booking request for Departing {model.Departing}, Returning {model.Returning}, StartLocation {model.StartLocation}," +
                                           $"EndLocation {model.EndLocation}, Duration {model.Duration}");

                    // POST method requires the Created result - return the newly booking request message, with its GET uri.
                    return Created(location, BookingOutputModel.FromBooking(booking));
                }
            }

            catch (Exception ex)
            {
                _logger.LogError("Failed Post booking request.", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Booking Request failure.");
            }

            return BadRequest();
        }
    }
}
