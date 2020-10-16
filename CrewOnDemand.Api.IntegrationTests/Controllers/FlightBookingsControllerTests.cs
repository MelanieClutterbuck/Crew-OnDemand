using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CrewOnDemand.Api.Models;
using CrewOnDemand.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CrewOnDemand.Api.IntegrationTests.Controllers
{
    public class FlightBookingsControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FlightBookingsControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            factory.ClientOptions.BaseAddress = new Uri("http://localhost/api/flightbookings");
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Post_WithValidBookingInputModel_ReturnsCreatedWithLocation()
        {
            var id = new Random().Next(int.MaxValue);
            var content = GetValidInputBookingJsonContent(id);

            var response = await _client.PostAsync("", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal($"/api/flightbookings/{id}", response.Headers.Location.ToString().ToLower());
        }

        [Fact] // sad path - one example of model validation testing ...
        public async Task Post_BookingInputModel_WithMissingId_ReturnsProblemDetails()
        {
            var id = new Random().Next(int.MaxValue);

            var model = GetValidBookingInputModel(id);
            model.StartLocation = string.Empty;

            await PostInvalidDataTestBody(model,
                new KeyValuePair<string, string>("StartLocation", "The StartLocation field is required."));
        }

        private static JsonContent GetValidInputBookingJsonContent(int id)
        {
            return JsonContent.Create(GetValidBookingInputModel(id));
        }

        private static BookingInputModel GetValidBookingInputModel(int id)
        {
            return new BookingInputModel()
            {
                Id = id,
                Departing = DateTime.Now.AddHours(4),
                Duration = 2,
                EndLocation = "Munich",
                Returning = DateTime.Now.AddHours(7),
                StartLocation = "Berlin"
            };
        }

        private async Task PostInvalidDataTestBody(BookingInputModel model, KeyValuePair<string, string> validationPair)
        {
            var content = JsonContent.Create(model);

            var response = await _client.PostAsync("", content);

            var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Single(problemDetails.Errors);
            Assert.True(problemDetails.Errors.ContainsKey(validationPair.Key));

            problemDetails.Errors.TryGetValue(validationPair.Key, out var detail);
            Assert.Equal(validationPair.Value, detail[0]);
        }
    }
}
