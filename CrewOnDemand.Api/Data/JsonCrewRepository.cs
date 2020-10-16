using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CrewOnDemand.Api.Data
{
    public class JsonCrewRepository : ICrewRepository
    {
        private readonly ILogger<JsonCrewRepository> _logger;
        private readonly IConfiguration _config;

        public JsonCrewRepository(IConfiguration config, ILogger<JsonCrewRepository> logger)
        {
            _config = config;
            _logger = logger;
        }
        
        public async Task<IReadOnlyList<CrewMember>> Get()
        {
            _logger.LogInformation("Started Get.");

            var file = _config.GetValue<string>("jsoncrew");

            var jsonString = await File.ReadAllTextAsync(file);

            using var document = JsonDocument.Parse(jsonString);

            var root = document.RootElement;
            var crew = root.GetProperty("Crew");

            var crewMembers = crew.EnumerateArray().Select
            (crewMember => JsonSerializer.Deserialize<CrewMember>(crewMember.ToString())).ToList();

            _logger.LogInformation("Completed Get.");

            return crewMembers.AsReadOnly();
        }
    }
}
