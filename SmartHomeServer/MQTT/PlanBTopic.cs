using Business.Abstract;
using Entities.Enums;
using SmartHomeServer.MQTT.Abstract;
using System.Text;
using Telegram.Bot;

namespace SmartHomeServer.MQTT;

[Topic(Topic = "planB")]
public class PlanBTopic : ITopic
{
    ITelegramBotClient _client;
    IStatService _statService;
    ILogService _logService;
    IUserService _userService;

    public PlanBTopic(IBotService botService, IStatService statService, ILogService logService, IUserService userService)
    {
        _client = new TelegramBotClient(botService.GetSelectedBot().Token);
        _statService = statService;
        _logService = logService;
        _userService = userService;
    }

    public void Execute(MQTTMessage message)
    {
        if (_statService.FirstOrDefalut(((int)EClient.PublicClient).ToString(), "alarm")?.Value == "1")
            _userService.GetAdmins().ForEach(e =>
            {
                _client.SendMessage(e.ChatId, "alarm: Plan B");
            });

        _logService.AddAsync(message.Topic, Encoding.Default.GetString(message.Payload), message.Sender);
    }
}
