using MediatR;
using TeamCounter.Application.Services;
using TeamCounter.Domain.Models;

namespace TeamCounter.Application.Handlers.GetTeams;

public class GetTeamsHandler(ITeamService teamService) : IRequestHandler<GetTeamsCommand, IEnumerable<Team>>
{
    public Task<IEnumerable<Team>> Handle(GetTeamsCommand request, CancellationToken cancellationToken)
    {
        var teams = teamService.GetTeams();
        
        return Task.FromResult(teams);
    }
}