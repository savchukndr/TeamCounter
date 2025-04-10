using MediatR;

namespace TeamCounter.Application.Handlers.CreateTeam;

public record CreateTeamCommand(string Name) : IRequest<Guid>;