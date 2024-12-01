using Entities.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotCore.Processes.Abstract;

namespace TelegramBotCore.Processes;

[Process(ChatTypeId = (int)ChatType.Supergroup, StepId = (int)EStep.GroupVideoFirstAlgorithm, StepIndexId = (int)EStep.Home)]
public class ChannelVideoProcess : IProcess
{
    public void Execute(Update update)
    {
        Console.WriteLine(update);
    }
}
