using TeamCounter.Application.Dtos;
using TeamCounter.Domain.Models;

namespace TeamCounter.Application.Services;

public interface ITeamService
{
    Guid CreateTeam(string name);
    
    IEnumerable<Team> GetTeams();
    
    Team? GetTeam(Guid teamId);
    
    void DeleteTeam(Guid teamId);
    
    Guid AddCounter(Team team, string counterName);
    
    Counter? GetCounter(Team team, Guid counterId);

    void DeleteCounter(Team team, Counter counter);
    
    void IncrementCounter(Team team, Counter counter, int steps);
    
    int GetTeamTotal(Team team);
    
    IEnumerable<TeamSummaryDto> GetLeaderboard();
    
    IEnumerable<CounterSummaryDto> GetCounters(Team team);
}