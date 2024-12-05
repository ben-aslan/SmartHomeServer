using Entities.Enums;
using Telegram.Bot;

namespace TelegramBotCore.Commands.Abstract;

public abstract class Command
{
    ITelegramBotClient _BotClient = null!;
    internal List<EOperationClaim> _claims = null!;
}
