using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.GetTeamTotal;

public class GetTeamTotalHandler(ITeamService teamService) : IRequestHandler<GetTeamTotalCommand, int>
{
    public Task<int> Handle(GetTeamTotalCommand request, CancellationToken cancellationToken)
    {
        var team = teamService.GetTeam(request.TeamId)
            ?? throw new KeyNotFoundException($"Team {request.TeamId} does not exist");
        
        var total = teamService.GetTeamTotal(team);
        
        return Task.FromResult(total);
    }
}