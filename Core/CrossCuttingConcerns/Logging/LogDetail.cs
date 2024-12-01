namespace Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string MethodName { get; set; } = null!;
    public List<LogParameter> LogParameters { get; set; } = null!;
    public DateTime LogDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time"));
}
