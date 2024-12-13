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
    public LampTopic(IBotService botService)
    {
        _client = new TelegramBotClient(botService.GetSelectedBot().Token);
    }

    public void Execute(MQTTMessage message)
    {
        var payload = Encoding.Default.GetString(message.Payload);
        Console.WriteLine(message);
        return;
    }
}
