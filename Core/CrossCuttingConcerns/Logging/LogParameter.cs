namespace Core.CrossCuttingConcerns.Logging;

public class LogParameter
{
    public string Name { get; set; } = null!;
    public object Value { get; set; } = null!;
    public string? Type { get; set; }
}
