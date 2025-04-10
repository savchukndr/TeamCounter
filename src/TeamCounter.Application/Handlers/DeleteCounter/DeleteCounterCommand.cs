using MediatR;

namespace TeamCounter.Application.Handlers.DeleteCounter;

public record DeleteCounterCommand(Guid TeamId, Guid CounterId) : IRequest; 