using MediatR;
using TeamCounter.Application.Dtos;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.GetLeaderBoard;

public class GetLeaderBoardHandler(ITeamService teamService)
    : IRequestHandler<GetLeaderBoardCommand, IEnumerable<TeamSummaryDto>>
{
    public Task<IEnumerable<TeamSummaryDto>> Handle(GetLeaderBoardCommand request, CancellationToken cancellationToken)
    {
        var leaderboard = teamService.GetLeaderboard();
        
        return Task.FromResult(leaderboard);
    }
}