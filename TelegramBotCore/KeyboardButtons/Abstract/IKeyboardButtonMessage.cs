using Telegram.Bot.Types;

namespace TelegramBotCore.KeyboardButtons.Abstract;

public interface IKeyboardButtonMessage
{
    Task Execute(Update update);
}
