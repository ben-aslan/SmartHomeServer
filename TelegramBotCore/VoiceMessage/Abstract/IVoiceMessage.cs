using Telegram.Bot.Types;

namespace TelegramBotCore.VoiceMessage.Abstract;

public interface IVoiceMessage
{
    Task Execute(Update update);
}
