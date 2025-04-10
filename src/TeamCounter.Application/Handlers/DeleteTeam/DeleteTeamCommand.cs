using MediatR;

namespace TeamCounter.Application.Handlers.DeleteTeam;

public record DeleteTeamCommand(Guid TeamId) : IRequest;