namespace TelegramBotCore.KeyboardButtons.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class KeyboardButtonMessageAttribute : Attribute
{
    public string Text { get; set; } = null!;
}
