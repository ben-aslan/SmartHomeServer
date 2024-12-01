using Business.LangService.Abstract;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotCore.VideoMessages.Abstract;

public interface IVideoMessage
{
    void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!);
}
