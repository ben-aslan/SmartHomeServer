using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBotAPI.Handle.Abstract;

public interface IHandle
{
    bool HandleKeyboardButton(Update update);
    void HandleCommand(Update update, long botId);
    void HandleMessage(Update update);
    void HandleVoiceMessage(Update update);
    void HandleCallBackQuery(Update update, long botId);
    void HandleVideoMessage(Update update, long botId);
}
