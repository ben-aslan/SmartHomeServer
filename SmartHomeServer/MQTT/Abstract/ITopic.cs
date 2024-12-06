namespace SmartHomeServer.MQTT.Abstract;

public interface ITopic
{
    void Execute(MQTTMessage message);
}
