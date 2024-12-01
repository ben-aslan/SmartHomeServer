using Telegram.Bot.Types;

namespace TelegramBotCore.Processes.Abstract;

public interface IProcess
{
    void Execute(Update update);
}
