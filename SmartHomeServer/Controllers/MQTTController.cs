using Business.Abstract;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using MQTTnet.Adapter;
using MQTTnet.AspNetCore;
using MQTTnet.Diagnostics;
using MQTTnet.Formatter;
using MQTTnet.Server;

namespace SmartHomeServer.Controllers;

public class MQTTController : ConnectionHandler, IMqttServerAdapter
{
    MqttServer _mqttServer;

    public MQTTController(MqttServer mqttServer)
    {
        _mqttServer = mqttServer;
        _serverOptions = null!;
        ClientHandler = null!;
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
