﻿using Business.Abstract;
using Entities.Enums;
using SmartHomeServer.MQTT.Abstract;
using System.Text;
using Telegram.Bot;

namespace SmartHomeServer.MQTT;

[Topic(Topic = "alarm")]
public class AlarmTopic : ITopic
{
    ITelegramBotClient _client;
    IStatService _statService;
    ILogService _logService;
    IUserService _userService;

    public AlarmTopic(IBotService botService, IStatService statService, ILogService logService, IUserService userService)
    {
        _client = new TelegramBotClient(botService.GetSelectedBot().Token);
        _statService = statService;
        _logService = logService;
        _userService = userService;
    }

    public void Execute(MQTTMessage message)
    {
        if (_statService.Any(((int)EClient.PublicClient).ToString(), message.Topic, Encoding.Default.GetString(message.Payload)))
            return;
        _userService.GetAdmins().ForEach(e =>
        {
            _client.SendMessage(e.ChatId, "Alarm: " + (Encoding.Default.GetString(message.Payload) == "1" ? "activated" : "deactivated"), disableNotification: true);
        });

        _statService.ChangeStatAsync(((int)EClient.PublicClient).ToString(), message.Topic, Encoding.Default.GetString(message.Payload));

        _logService.AddAsync(message.Topic, Encoding.Default.GetString(message.Payload), message.Sender);
    }
}
