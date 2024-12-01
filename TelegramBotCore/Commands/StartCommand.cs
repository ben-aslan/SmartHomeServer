using Business.Abstract;
using Telegram.Bot.Types;
using TelegramBotCore.Commands.Abstract;
using Entities.Concrete;
using User = Entities.Concrete.User;
using Telegram.Bot;
using Business.LangService.Abstract;
using Entities.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotCore.Commands;

[Command(Name = "/start")]
public class StartCommand : Command, ICommand
{
    IUserService _userService;

    public StartCommand(IUserService userService)
    {
        _userService = userService;
    }

    public void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!)
    {
        var message = update.Message!;
        var result = _userService.Add(new User
        {
            FirstName = message.Chat.FirstName!,
            LastName = message.Chat.LastName,
            UserName = message.Chat.Username,
            ChatId = message.Chat.Id,
            IsBot = message.From?.IsBot ?? false,
            IsPremium = message.From?.IsPremium ?? false,
            UserTag = message.Chat.FirstName,
            AddedToAttachmentMenu = message.From?.AddedToAttachmentMenu ?? false,
            CanJoinGroups = message.From?.CanJoinGroups ?? false,
            CanReadAllGroupMessages = message.From?.CanReadAllGroupMessages ?? false,
            SupportsInlineQueries = message.From?.SupportsInlineQueries ?? false,
            LanguageId = (int)ELang.FA
        });

        var userOperationalClaim = _userService.GetUserClaim(message.From!.Id).Data;

        if (userOperationalClaim == EOperationClaim.Admin)
        {
            _client.SendMessage(message.Chat.Id, "سلام، خوش آمدید!🌹\n" + _message.Get(EMessage.WellcomeMessage), replyMarkup: new ReplyKeyboardMarkup(new List<List<KeyboardButton>> {
                new() { new(_message.Get(EMessage.UploadVideo1)), new (_message.Get(EMessage.UploadVideo2)) }
            }));
            return;
        }

        _client.SendMessage(message.Chat.Id, _message.GetByName("first-page"));
    }
}
