using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Server;
using System.Diagnostics.Metrics;

namespace SmartHomeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        MqttServer _mqttServer;

        public ValueController(MqttServer mqttServer)
        {
            _mqttServer = mqttServer;
        }

        [HttpGet("lampOn")]
        public IActionResult LampOn()
        {
            _mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(new MqttApplicationMessageBuilder().WithTopic("lamp").WithPayload("0").Build())
            {
                SenderClientId = "Server"
            });
            return Ok();
        }

        [HttpGet("lampOff")]
        public IActionResult LampOff()
        {
            _mqttServer.InjectApplicationMessage(new InjectedMqttApplicationMessage(new MqttApplicationMessageBuilder().WithTopic("lamp").WithPayload("1").Build())
            {
                SenderClientId = "Server"
            });
            return Ok();
        }
    }
}
