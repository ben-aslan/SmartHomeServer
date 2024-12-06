using Entities.Enums;
using Telegram.Bot.Types.Enums;

namespace SmartHomeServer.MQTT.Abstract;

[AttributeUsage(AttributeTargets.Class)]
public class TopicAttribute : Attribute
{
    public string Topic { get; set; } = "no_topic";
}
