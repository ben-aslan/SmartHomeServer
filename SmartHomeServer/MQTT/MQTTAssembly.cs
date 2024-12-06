using System.Reflection;

namespace SmartHomeServer.MQTT;

public static class MQTTAssembly
{
    public static Assembly GetAssembly => Assembly.GetExecutingAssembly();
}
