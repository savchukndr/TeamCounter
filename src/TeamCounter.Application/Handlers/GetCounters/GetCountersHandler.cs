using MediatR;
using TeamCounter.Application.Dtos;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.GetCounters;

public class GetCountersHandler(ITeamService teamService) : IRequestHandler<GetCountersCommand, IEnumerable<CounterSummaryDto>>
{
    public Task<IEnumerable<CounterSummaryDto>> Handle(GetCountersCommand request, CancellationToken cancellationToken)
    {
        var team = teamService.GetTeam(request.TeamId)
            ?? throw new KeyNotFoundException($"Team {request.TeamId} does not exist");

        var counters = teamService.GetCounters(team);
        
        return Task.FromResult(counters);
    }
}