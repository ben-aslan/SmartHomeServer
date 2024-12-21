using Business.Abstract;
using Entities.Enums;
using SmartHomeServer.MQTT.Abstract;
using System.Text;
using Telegram.Bot;

namespace SmartHomeServer.MQTT;

[Topic(Topic = "lamp")]
public class LampTopic : ITopic
{
    ITelegramBotClient _client;
    IStatService _statService;
    ILogService _logService;

    public LampTopic(IBotService botService, IStatService statService, ILogService logService)
    {
        _client = new TelegramBotClient(botService.GetSelectedBot().Token);
        _statService = statService;
        _logService = logService;
    }

    public void Execute(MQTTMessage message)
    {
        if (_statService.Any(message.Sender, message.Topic, Encoding.Default.GetString(message.Payload)))
            return;

        _statService.ChangeStatAsync(((int)EClient.PublicClient).ToString(), message.Topic, Encoding.Default.GetString(message.Payload));

        _logService.AddAsync(message.Topic, Encoding.Default.GetString(message.Payload), message.Sender);
    }
}
