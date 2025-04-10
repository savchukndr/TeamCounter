using MediatR;

namespace TeamCounter.Application.Handlers.GetTeamTotal;

public record GetTeamTotalCommand(Guid TeamId) : IRequest<int>;