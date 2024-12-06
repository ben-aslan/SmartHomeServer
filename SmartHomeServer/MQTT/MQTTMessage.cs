namespace SmartHomeServer.MQTT;

public class MQTTMessage
{
    public string Topic { get; set; } = null!;
    public byte[] Payload { get; set; } = null!;
}
