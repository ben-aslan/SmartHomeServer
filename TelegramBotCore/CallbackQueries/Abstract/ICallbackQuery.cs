using Business.LangService.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotCore.CallbackQueries.Abstract;

public interface ICallbackQuery
{
    void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!);
}
