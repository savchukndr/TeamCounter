namespace TeamCounter.Application.Dtos;

public class CounterSummaryDto
{
    public Guid CounterId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Steps { get; set; }
}