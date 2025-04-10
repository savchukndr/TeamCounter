using MediatR;
using TeamCounter.Domain.Models;

namespace TeamCounter.Application.Handlers.GetTeams;

public record GetTeamsCommand : IRequest<IEnumerable<Team>>;