using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.DeleteCounter;

public class DeleteCounterHandler(ITeamService teamService) : IRequestHandler<DeleteCounterCommand>
{
    public Task Handle(DeleteCounterCommand request, CancellationToken cancellationToken)
    {
        var team = teamService.GetTeam(request.TeamId)
            ?? throw new KeyNotFoundException($"Team {request.TeamId} does not exist");

        var counter = teamService.GetCounter(team, request.CounterId)
            ?? throw new KeyNotFoundException($"Team {team.Name} does not have counter with id: {request.CounterId}");
            
        teamService.DeleteCounter(team, counter);
        
        return Task.CompletedTask;
    }
}