using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.DeleteTeam;

public class DeleteTeamHandler(ITeamService teamService) : IRequestHandler<DeleteTeamCommand>
{
    public Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        teamService.DeleteTeam(request.TeamId);
        
        return Task.CompletedTask;
    }
}