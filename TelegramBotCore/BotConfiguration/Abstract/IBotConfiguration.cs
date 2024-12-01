using Entities.Dtos;
using IResult = Core.Utilities.Results.IResult;

namespace TelegramBotAPI.BotConfiguration.Abstract;

public interface IBotConfiguration
{
    Task<IResult> SetWebhook(CancellationToken ct, IProgress<ProgressReportDto> progress);
    //TelegramBotClient GetClient();
}
