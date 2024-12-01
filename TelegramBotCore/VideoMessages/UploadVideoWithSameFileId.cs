using Business.Abstract;
using Business.LangService.Abstract;
using Entities.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotCore.VideoMessages.Abstract;

namespace TelegramBotCore.VideoMessages;

[VideoMessage(StepId = (int)EStep.Video, StepIndexId = (int)EStepIndex.UploadWithSameFileId)]
public class UploadVideoWithSameFileId : VideoMessage, IVideoMessage
{
    IGroupService _groupService;
    IBotService _botService;

    public UploadVideoWithSameFileId(IGroupService groupService, IBotService botService)
    {
        _groupService = groupService;
        _botService = botService;
    }

    public void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!)
    {
        if (!_botService.IsUploadManager(_client.BotId))
            return;

        var group = _groupService.GetSelectedGroup();

        _client.SendVideo(group.ChatId, InputFile.FromFileId(update.Message!.Video!.FileId), disableNotification: true);
    }
}
