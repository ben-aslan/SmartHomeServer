using Autofac;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilities.Results;
using Entities.Dtos;
using Telegram.Bot;
using TelegramBotAPI.BotConfiguration.Abstract;
using TelegramBotAPI.DependencyResolvers.Abstract;
using TelegramBotAPI.Handle.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace TelegramBotAPI.BotConfiguration;

public class BotConfiguration : IBotConfiguration
{
    IHandle _handle;
    IDependencyResolver _resolver;
    IBotService _botService;

    public BotConfiguration(IHandle handle, IDependencyResolver resolver)
    {
        _handle = handle;
        _resolver = resolver;
        _botService = resolver.BaseContainer.Resolve<IBotService>();
    }

    [SecuredOperation("admin")]
    public async Task<IResult> SetWebhook(CancellationToken ct, IProgress<ProgressReportDto> progress)
    {
        ProgressReportDto progressReport = new();

        var bots = _botService.GetActiveBots();

        int perc = 100 / bots.Count;

        await Parallel.ForEachAsync(bots, ct, async (bot, ct) =>
        {
            await new TelegramBotClient(bot.Token).SetWebhook(string.Format(bot.WebhookUrl + "{0}", "/api/handler?botId=" + bot.BotChatId));

            ct.ThrowIfCancellationRequested();
            progressReport.PercentageComplete += perc;
            progressReport.Description = "Setting webhooks".ToUpper();
            progress.Report(progressReport);
        });

        return new SuccessResult("Webhooks was seted");
    }
}
