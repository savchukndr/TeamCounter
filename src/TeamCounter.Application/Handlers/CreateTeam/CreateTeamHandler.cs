using MediatR;
using TeamCounter.Application.Services;

namespace TeamCounter.Application.Handlers.CreateTeam;

public class CreateTeamHandler(ITeamService teamService) : IRequestHandler<CreateTeamCommand, Guid>
{
    public Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var teamId = teamService.CreateTeam(request.Name);
        
        return Task.FromResult(teamId);
    }
}