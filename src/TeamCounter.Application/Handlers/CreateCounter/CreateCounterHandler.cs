using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.CreateCounter;

public class CreateCounterHandler(ITeamService teamService) : IRequestHandler<CreateCounterCommand, Guid>
{
    public Task<Guid> Handle(CreateCounterCommand request, CancellationToken cancellationToken)
    {
        var team = teamService.GetTeam(request.TeamId)
            ?? throw new KeyNotFoundException($"Team {request.TeamId} does not exist.");
        
        var counterId = teamService.AddCounter(team, request.Name);
        
        return Task.FromResult(counterId);
    }
}