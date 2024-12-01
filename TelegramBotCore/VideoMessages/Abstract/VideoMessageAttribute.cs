namespace TelegramBotCore.VideoMessages.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class VideoMessageAttribute : Attribute
{
    public int StepId { get; set; } = 0;
    public int StepIndexId { get; set; } = 0;
    public int ChatTypeId { get; set; } = 0;
    public string Key { get { return StepId.ToString() + "_" + StepIndexId.ToString() + "_" + ChatTypeId.ToString(); } }
}
