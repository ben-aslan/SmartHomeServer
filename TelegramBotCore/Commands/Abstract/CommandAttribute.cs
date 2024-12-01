using Telegram.Bot.Types.Enums;

namespace TelegramBotCore.Commands.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : Attribute
{
    public string Name { get; set; } = "no_name";
    public ChatType ChatType { get; set; } = ChatType.Private;
}
