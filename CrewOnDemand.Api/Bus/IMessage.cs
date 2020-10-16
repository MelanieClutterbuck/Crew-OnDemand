using System;

namespace CrewOnDemand.Api.Bus
{
    public interface IMessage
    {
        Guid Id { get; }
        string Message { get; }
    }
}