using Business.Abstract;
using Business.LangService.Abstract;
using Entities.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotCore.Commands.Abstract;

namespace TelegramBotCore.Commands;

[Command(Name = "/firstalgorithm")]
public class FirstAlgorithmCommand : Command, ICommand
{
    IUserService _userService;
    IUserStepService _userStepService;
    IBotService _botService;

    public FirstAlgorithmCommand(IUserService userService, IUserStepService userStepService, IBotService botService)
    {
        _userService = userService;
        _userStepService = userStepService;
        _botService = botService;
    }

    public void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!)
    {
        if (!(_botService.IsUploadManager(_client.BotId) && _userService.HaveClaim(update.Message!.From!.Id, EOperationClaim.Admin)))
            return;

        _userStepService.Set(update.Message!.From!.Id, EStep.Video, EStepIndex.UploadWithSameFileId);

        _client.SendMessage(update.Message.Chat.Id, _message.Get(EMessage.UploadVideo1));
    }
}
