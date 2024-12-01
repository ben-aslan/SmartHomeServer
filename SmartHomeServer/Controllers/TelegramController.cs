using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Server;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotAPI.Handle.Abstract;

namespace SmartHomeServer.Controllers;

[Route("api/t")]
[ApiController]
public class TelegramController : ControllerBase
{
    IHandle _handle;
    MqttServer _mqttServer;

    public TelegramController(IHandle handle, MqttServer mqttServer)
    {
        _handle = handle;
        _mqttServer = mqttServer;
    }

    [HttpGet("sw")]
    public async Task<IActionResult> SetWebhookAsync(string botToken, string url)
    {
        await new TelegramBotClient(botToken).SetWebhook(url + "/api/update?botId=" + botToken.Split(':')[0]);
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(Update update, [FromQuery] long botId)
    {
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (update.Message!.Type == Telegram.Bot.Types.Enums.MessageType.Video)
                _handle.HandleVideoMessage(update, botId);

            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Voice)
            {
                _handle.HandleVoiceMessage(update);
            }
            else if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (_handle.HandleKeyboardButton(update))
                {
                    return Ok();
                }
                if (update.Message.Entities != null)
                {
                    if (update.Message.Entities[0].Type == Telegram.Bot.Types.Enums.MessageEntityType.BotCommand)
                    {
                        _handle.HandleCommand(update, botId);
                    }
                }
                else
                {
                    _handle.HandleMessage(update);
                }
            }
        }
        else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            //if (await new JoinValidator(_client, new ChannelManager(new EfChannelRepository())).IsJoin(update))
            //{
            if (update.CallbackQuery!.Message!.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private)
            {
                _handle.HandleCallBackQuery(update, botId);
            }
            //}
        }
        return Ok();
    }
}
