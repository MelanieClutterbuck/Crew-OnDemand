using System;

namespace CrewOnDemand.Api.Services
{
    public interface IPilotFinder
    {
        int SelectPilot(CrewLists crewLists, DateTime departing);
    }
}