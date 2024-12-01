using Business.LangService.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotCore.Commands.Abstract;

public interface ICommand
{
    void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!);
}
