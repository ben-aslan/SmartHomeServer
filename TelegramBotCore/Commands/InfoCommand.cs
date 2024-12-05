using Business.LangService.Abstract;
using Entities.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotCore.Commands.Abstract;

namespace TelegramBotCore.Commands;

[Command(Name = "/info")]
public class InfoCommand : Command, ICommand
{
    public void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!)
    {
        if (!_claims.Any(x => x == EOperationClaim.User))
            return;

    }
}
