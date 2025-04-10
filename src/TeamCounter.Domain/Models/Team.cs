namespace TeamCounter.Domain.Models;

public class Team
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;

    public List<Counter> Counters { get; } = [];

    public int TotalSteps => Counters.Sum(c => c.Steps);
}