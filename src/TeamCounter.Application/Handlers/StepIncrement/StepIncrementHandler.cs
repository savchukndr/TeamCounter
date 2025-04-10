using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.StepIncrement;

public class StepIncrementHandler(ITeamService teamService) : IRequestHandler<StepIncrementCommand>
{
    public Task Handle(StepIncrementCommand request, CancellationToken cancellationToken)
    {
        var team = teamService.GetTeam(request.TeamId)
            ?? throw new KeyNotFoundException($"Team {request.TeamId} does not exist");

        var counter = teamService.GetCounter(team, request.CounterId)
            ?? throw new KeyNotFoundException($"Team {team.Name} does not have counter with id: {request.CounterId}");
        
        teamService.IncrementCounter(team, counter, request.Steps);
        
        return Task.CompletedTask;
    }
}