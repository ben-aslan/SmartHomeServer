using Business.Abstract;
using Business.LangService.Abstract;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotCore.CallbackQueries.Abstract;
using TelegramBotCore.LangService.Concrete;

namespace TelegramBotCore.CallbackQueries;

[CallbackQueries(FunctionCode = "setLang")]
public class SetLangCallbackQuery : ICallbackQuery
{
    IUserService _userService;
    IWebHostEnvironment _environment;

    public SetLangCallbackQuery(IUserService userService, IWebHostEnvironment hostEnvironment)
    {
        _userService = userService;
        _environment = hostEnvironment;
    }

    public void Execute(Update update, ITelegramBotClient _client = null!, IMessageService _message = null!)
    {
        var lang = (ELang)Convert.ToInt32((update.CallbackQuery!.Data ?? " ").Split(" ")[1]);
        _userService.SetUserLang(lang, update.CallbackQuery.From.Id);

        _client.DeleteMessage(update.CallbackQuery.From.Id, update.CallbackQuery.Message!.MessageId);

        List<List<KeyboardButton>> keyboardButtons = new() {
            new() { new("🛍 خرید سرویس"),new("⚖️ تست سرویس") },
            new() { new("🛜 سرویس های من"),new("👤 مشخصات من") },
            new() { new("📚 آموزش"),new("💰 کیف پول")},
            new(){new("☎️ پشتیبانی") }
        };

        _client.SendMessage(update.CallbackQuery.From.Id, new JsonMessageManager(lang, _environment).GetByName("wellcome-message"), replyMarkup: new ReplyKeyboardMarkup(keyboardButtons));
    }
}
