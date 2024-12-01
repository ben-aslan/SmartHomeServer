using Autofac;
using Telegram.Bot.Types;
using TelegramBotCore.CallbackQueries.Abstract;
using TelegramBotCore.Commands.Abstract;
using TelegramBotAPI.DependencyResolvers.Abstract;
using TelegramBotAPI.Handle.Abstract;
using TelegramBotCore.KeyboardButtons.Abstract;
using TelegramBotCore.Processes.Abstract;
using TelegramBotCore.VoiceMessage.Abstract;
using Business.Abstract;
using System.Diagnostics;
using Telegram.Bot;
using TelegramBotCore.LangService.Concrete;
using TelegramBotCore.VideoMessages.Abstract;
using Microsoft.AspNetCore.Hosting;

namespace TelegramBotAPI.Handle;

public class UpdateHandle : IHandle
{
    IDependencyResolver _dependencyResolver;
    IUserService _userService;
    IUserStepService _userStepService;
    IBotService _botService;
    IWebHostEnvironment _environment;

    public UpdateHandle(IDependencyResolver dependencyResolver, IWebHostEnvironment environment)
    {
        _dependencyResolver = dependencyResolver;
        _userService = dependencyResolver.BaseContainer.Resolve<IUserService>();
        _environment = environment;
        _userStepService = dependencyResolver.BaseContainer.Resolve<IUserStepService>();
        _botService = dependencyResolver.BaseContainer.Resolve<IBotService>();
    }
    public void HandleCallBackQuery(Update update, long botId)
    {
        CallbackQuery callbackQuery = update.CallbackQuery!;

        IContainer container = _dependencyResolver.CallbackQueryContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                string key = (callbackQuery.Data ?? "").Split(" ")[0];
                var callbackQueryApp = scope.ResolveKeyed<ICallbackQuery>(key);
                callbackQueryApp.Execute(update, new TelegramBotClient(_botService.GetTokenByChatId(botId)), new JsonMessageManager(_userService.GetUserLang(callbackQuery.From?.Id ?? callbackQuery.Message!.Chat.Id).Data, _environment));
            }
            catch (Exception)
            {
            }
        }
    }

    public void HandleCommand(Update update, long botId)
    {
        Message message = update.Message!;

        IContainer container = _dependencyResolver.CommandContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                if ((message.Text ?? "").StartsWith("/start "))
                {
                    message.Text = "/" + message.Text!.Split(" ")[1];
                }
                string key = (message.Text ?? "").Split("_")[0];
                var command = scope.ResolveKeyed<ICommand>(key + "/*" + (int)message.Chat.Type);
                command.Execute(update, new TelegramBotClient(_botService.GetTokenByChatId(botId)), new JsonMessageManager(_userService.GetUserLang(message.From?.Id ?? message.Chat.Id).Data, _environment));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }

    public bool HandleKeyboardButton(Update update)
    {
        Message message = update.Message!;

        IContainer container = _dependencyResolver.KeyboardButtonMessageContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                string key = message.Text ?? "";
                var keyboardButtonMessage = scope.ResolveKeyed<IKeyboardButtonMessage>(key);
                var result = keyboardButtonMessage.Execute(update);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public void HandleMessage(Update update)
    {
        IContainer container = _dependencyResolver.ProcessContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                var userStep = _userStepService.Get(update.Message!.From!.Id);
                string key = userStep.StepId.ToString() + "_"
                    + userStep.StepIndexId.ToString() + "_" + ((int)update.Message!.Chat.Type).ToString();
                var process = scope.ResolveKeyed<IProcess>(key);
                process.Execute(update);
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }

    public void HandleVideoMessage(Update update, long botId)
    {
        var container = _dependencyResolver.VideoMessageContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                var userStep = _userStepService.Get(update.Message!.From!.Id);
                string key = userStep.StepId.ToString() + "_"
                    + userStep.StepIndexId.ToString() + "_" + ((int)update.Message!.Chat.Type).ToString();
                var VideoMessage = scope.ResolveKeyed<IVideoMessage>(key);
                VideoMessage.Execute(update, new TelegramBotClient(_botService.GetTokenByChatId(botId)), new JsonMessageManager(_userService.GetUserLang(update.Message!.From?.Id ?? update.Message!.Chat.Id).Data, _environment));
            }
            catch (Exception)
            {

            }
        }
    }

    public void HandleVoiceMessage(Update update)
    {
        IContainer container = _dependencyResolver.VoiceMessageContainer;

        using (var scope = container.BeginLifetimeScope())
        {
            try
            {
                var userStep = _userStepService.Get(update.Message!.From!.Id);
                string key = userStep.StepId.ToString() + "_"
                    + userStep.StepIndexId.ToString() + "_" + ((int)update.Message!.Chat.Type).ToString();
                var voiceMessage = scope.ResolveKeyed<IVoiceMessage>(key);
                var result = voiceMessage.Execute(update);
            }
            catch (Exception)
            {

            }
        }
    }
}
