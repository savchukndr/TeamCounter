namespace TeamCounter.Application.Dtos;

public class TeamSummaryDto
{
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TotalSteps { get; set; }
}