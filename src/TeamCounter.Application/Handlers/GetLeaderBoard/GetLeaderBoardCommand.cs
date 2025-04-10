using MediatR;
using TeamCounter.Application.Dtos;

namespace TeamCounter.Application.Handlers.GetLeaderBoard;

public record GetLeaderBoardCommand : IRequest<IEnumerable<TeamSummaryDto>>;