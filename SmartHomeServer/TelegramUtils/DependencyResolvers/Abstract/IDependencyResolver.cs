using Autofac;

namespace SmartHomeServer.TelegramUtils.DependencyResolvers.Abstract;

public interface IDependencyResolver
{
    public IContainer BaseContainer { get; }
    public IContainer CommandContainer { get; }
    public IContainer ProcessContainer { get; }
    public IContainer VoiceMessageContainer { get; }
    public IContainer VideoMessageContainer { get; }
    public IContainer KeyboardButtonMessageContainer { get; }
    public IContainer CallbackQueryContainer { get; }
    public IContainer MQTTContainer { get; }
    ContainerBuilder GetBasicRegisters(ContainerBuilder builder = null!);
    IContainer GetCommandContainer();
    IContainer GetProcessContainer();
    IContainer GetVoiceMessageContainer();
    IContainer GetVideoMessageContainer();
    IContainer GetKeyboardButtonMessageContainer();
    IContainer GetCallbackQueryContainer();
    IContainer GetControllerContainer();
    IContainer GetMQTTContainer();
}
