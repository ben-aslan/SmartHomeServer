using Business.Abstract;
using SmartHomeServer.MQTT.Abstract;
using System.Text;
using Telegram.Bot;

namespace SmartHomeServer.MQTT;

[Topic(Topic = "commode1")]
public class Commode1Topic : ITopic
{
    ITelegramBotClient _client;
    IStatService _statService;
    ILogService _logService;
    IUserService _userService;

    public Commode1Topic(IBotService botService, IStatService statService, ILogService logService, IUserService userService)
    {
        _client = new TelegramBotClient(botService.GetSelectedBot().Token);
        _statService = statService;
        _logService = logService;
        _userService = userService;
    }

    public void Execute(MQTTMessage message)
    {
        if (_statService.Any(message.Sender, message.Topic, Encoding.Default.GetString(message.Payload)))
            return;
        if (_statService.FirstOrDefalut(message.Sender, "alarm")?.Value == "1")
            _userService.GetAdmins().ForEach(e =>
            {
                _client.SendMessage(e.ChatId, "commode 1 " + (Encoding.Default.GetString(message.Payload) == "0" ? "opended" : "closed"));
            });

        _statService.ChangeStatAsync(message.Sender, message.Topic, Encoding.Default.GetString(message.Payload));

        _logService.AddAsync(message.Topic, Encoding.Default.GetString(message.Payload), message.Sender);
    }
}
