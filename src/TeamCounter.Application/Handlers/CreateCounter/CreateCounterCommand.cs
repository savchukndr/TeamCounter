using MediatR;

namespace TeamCounter.Application.Handlers.CreateCounter;

public record CreateCounterCommand(Guid TeamId, string Name) : IRequest<Guid>;