namespace TeamCounter.Domain.Models;

public class Counter
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public int Steps { get; private set; } = 0;

    public void Increment(int steps)
    {
        if (steps <= 0)
            throw new ArgumentException("Steps must be greater than 0.");

        Steps += steps;
    }
}