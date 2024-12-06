using Autofac;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Hosting.Server;
using MQTTnet;
using MQTTnet.Adapter;
using MQTTnet.AspNetCore;
using MQTTnet.Diagnostics;
using MQTTnet.Formatter;
using MQTTnet.Server;
using SmartHomeServer.MQTT;
using SmartHomeServer.MQTT.Abstract;
using SmartHomeServer.TelegramUtils.DependencyResolvers.Abstract;
using Telegram.Bot.Types;
using TelegramBotCore.KeyboardButtons.Abstract;

namespace SmartHomeServer.Controllers;

public class MQTTController : ConnectionHandler, IMqttServerAdapter
{
    MqttServer _mqttServer;
    IDependencyResolver _dependencyResolver;

    public MQTTController(MqttServer mqttServer, IDependencyResolver dependencyResolver)
    {
        _mqttServer = mqttServer;
        _dependencyResolver = dependencyResolver;
        _mqttServer.InterceptingPublishAsync += InterceptingPublishAsync;
        _mqttServer.InterceptingSubscriptionAsync += InterceptingSubscriptionAsync;
        _mqttServer.ClientAcknowledgedPublishPacketAsync += _mqttServer_ClientAcknowledgedPublishPacketAsync;
        _mqttServer.InterceptingClientEnqueueAsync += InterceptingClientEnqueueAsync;
        _serverOptions = null!;
        ClientHandler = null!;
    }

    private async Task InterceptingClientEnqueueAsync(InterceptingClientApplicationMessageEnqueueEventArgs arg)
    {
        if (arg.SenderClientId != "SenderClientId")
            await _mqttServer.InjectApplicationMessage(new(new MqttApplicationMessageBuilder().WithTopic(arg.ApplicationMessage.Topic).WithPayload("InterceptingClientEnqueueAsync").Build())
            {
                SenderClientId = "SenderClientId"
            });
    }

    private async Task _mqttServer_ClientAcknowledgedPublishPacketAsync(ClientAcknowledgedPublishPacketEventArgs arg)
    {
        if (arg.ClientId != "SenderClientId")
            await _mqttServer.InjectApplicationMessage(new(new MqttApplicationMessageBuilder().WithTopic(arg.PublishPacket.Topic).WithPayload("ClientAcknowledgedPublishPacket").Build())
            {
                SenderClientId = "SenderClientId"
            });
    }

    private async Task InterceptingSubscriptionAsync(InterceptingSubscriptionEventArgs arg)
    {
        if (arg.ClientId != "SenderClientId")
            await _mqttServer.InjectApplicationMessage(new(new MqttApplicationMessageBuilder().WithTopic(arg.TopicFilter.Topic).WithPayload("InterceptingSubscription").Build())
            {
                SenderClientId = "SenderClientId"
            });
    }

    private Task InterceptingPublishAsync(InterceptingPublishEventArgs arg)
    {
        if (arg.ClientId != "SenderClientId")
        {
            IContainer container = _dependencyResolver.MQTTContainer;

            using (var scope = container.BeginLifetimeScope())
            {
                try
                {
                    string key = arg.ApplicationMessage.Topic ?? "";
                    var topic = scope.ResolveKeyed<ITopic>(key);
                    topic.Execute(new MQTTMessage { Topic = arg.ApplicationMessage.Topic, Payload = arg.ApplicationMessage.Payload });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        return Task.CompletedTask;
    }

    MqttServerOptions _serverOptions;

    public Func<IMqttChannelAdapter, Task> ClientHandler { get; set; }

    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        // required for websocket transport to work
        var transferFormatFeature = connection.Features.Get<ITransferFormatFeature>();
        if (transferFormatFeature != null)
        {
            transferFormatFeature.ActiveFormat = TransferFormat.Binary;
        }

        var formatter = new MqttPacketFormatterAdapter(new MqttBufferWriter(_serverOptions.WriterBufferSize, _serverOptions.WriterBufferSizeMax));
        using (var adapter = new MqttConnectionContext(formatter, connection))
        {
            var clientHandler = ClientHandler;
            if (clientHandler != null)
            {
                await clientHandler(adapter).ConfigureAwait(false);
            }
        }
    }

    public Task StartAsync(MqttServerOptions options, IMqttNetLogger logger)
    {
        _serverOptions = options;

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}
