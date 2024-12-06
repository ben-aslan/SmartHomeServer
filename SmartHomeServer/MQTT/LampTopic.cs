using Entities.Enums;
using SmartHomeServer.MQTT.Abstract;
using System.Text;

namespace SmartHomeServer.MQTT;

[Topic(Topic = "lamp")]
public class LampTopic : ITopic
{
    public void Execute(MQTTMessage message)
    {
        var a = Encoding.Default.GetString(message.Payload);
        Console.WriteLine(message);
        return;
    }
}
