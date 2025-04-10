using System.Collections.Concurrent;
using TeamCounter.Application.Dtos;
using TeamCounter.Domain.Models;

namespace TeamCounter.Application.Services;

public class TeamService : ITeamService
{
    private readonly ConcurrentDictionary<Guid, Team> _teams = new();

    // synchronization object to protect shared resources in lock statements
    private readonly object _lockObject = new();

    public Guid CreateTeam(string name)
    {
        if (_teams.Any(t => t.Value.Name == name))
        {
            throw new ArgumentException($"Team with name {name} already exists");
        }
        
        var team = new Team
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        _teams[team.Id] = team;
        
        return team.Id;
    }
    
    public IEnumerable<Team> GetTeams() => _teams.Values;
    
    public Team? GetTeam(Guid teamId) => _teams.GetValueOrDefault(teamId);

    public void DeleteTeam(Guid teamId)
    {
        _teams.TryRemove(teamId, out _);
    }

    public Guid AddCounter(Team team, string counterName)
    {
        if (team.Counters.Any(c => c.Name == counterName))
        {
            throw new ArgumentException($"Counter with name {counterName} already exists");
        }
        
        var counter = new Counter
        {
            Id = Guid.NewGuid(),
            Name = counterName
        };

        lock (_lockObject)
        {
            team.Counters.Add(counter);
        }

        return counter.Id;
    }

    public Counter? GetCounter(Team team, Guid counterId) =>
        team.Counters.FirstOrDefault(c => c.Id == counterId);

    public void DeleteCounter(Team team, Counter counter)
    {
        lock (_lockObject)
        {
            team.Counters.Remove(counter);
        }
    }

    public void IncrementCounter(Team team, Counter counter, int steps)
    {
        lock (_lockObject)
        {
            counter.Increment(steps);
        }
    }

    public int GetTeamTotal(Team team) => team.TotalSteps;

    public IEnumerable<TeamSummaryDto> GetLeaderboard() =>
        _teams.Values
            .OrderByDescending(t => t.TotalSteps)
            .Select(t => new TeamSummaryDto
            {
                TeamId = t.Id,
                Name = t.Name,
                TotalSteps = t.TotalSteps
            });

    public IEnumerable<CounterSummaryDto> GetCounters(Team team)
    {
        lock (_lockObject)
        {
            return team.Counters
                .Select(c => new CounterSummaryDto
                {
                    CounterId = c.Id,
                    Name = c.Name,
                    Steps = c.Steps
                })
                .ToList();
        }
    }
}