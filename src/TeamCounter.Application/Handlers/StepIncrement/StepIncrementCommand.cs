using MediatR;

namespace TeamCounter.Application.Handlers.StepIncrement;

public record StepIncrementCommand(Guid TeamId, Guid CounterId, int Steps) : IRequest;