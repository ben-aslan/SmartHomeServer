using System;

namespace SmartHomeServer.MQTT;

public class MQTTMessage
{
    public string Topic { get; set; } = null!;
    public ArraySegment<byte> Payload { get; set; } = null!;
    public string Sender { get; set; } = null!;
}
