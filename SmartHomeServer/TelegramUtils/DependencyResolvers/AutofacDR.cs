using Autofac;
using Autofac.Extras.DynamicProxy;
using Business;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Enums;
using SmartHomeServer.MQTT;
using SmartHomeServer.MQTT.Abstract;
using SmartHomeServer.TelegramUtils.BotConfiguration.Abstract;
using SmartHomeServer.TelegramUtils.DependencyResolvers.Abstract;
using System.Reflection;
using Telegram.Bot.Types.Enums;
using TelegramBotCore;
using TelegramBotCore.CallbackQueries.Abstract;
using TelegramBotCore.Commands.Abstract;
using TelegramBotCore.KeyboardButtons.Abstract;
using TelegramBotCore.Processes.Abstract;
using TelegramBotCore.VideoMessages.Abstract;
using TelegramBotCore.VoiceMessage.Abstract;
using Module = Autofac.Module;

namespace SmartHomeServer.TelegramUtils.DependencyResolvers;

public class AutofacDR : Module, IDependencyResolver
{
    IWebHostEnvironment _environment;
    IConfiguration _configuration;
    Assembly _asm;

    public AutofacDR(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _asm = TelegramBotCoreAssembly.GetAssembly;
        _environment = environment;
        _configuration = configuration;
        BaseContainer = GetBasicRegisters().Build();
        CommandContainer = GetCommandContainer();
        ProcessContainer = GetProcessContainer();
        VoiceMessageContainer = GetVoiceMessageContainer();
        KeyboardButtonMessageContainer = GetKeyboardButtonMessageContainer();
        CallbackQueryContainer = GetCallbackQueryContainer();
        VideoMessageContainer = GetVideoMessageContainer();
        MQTTContainer = GetMQTTContainer();
    }

    protected override void Load(ContainerBuilder builder)
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        builder = GetBasicRegisters(builder);

        builder.RegisterAssemblyTypes(asm)
           .Where(x => x.GetInterface("ICallbackQuery") == typeof(ICallbackQuery) && x.IsClass)
           .As<ICallbackQuery>()
           .SingleInstance()
           .Keyed<ICallbackQuery>(x => x.GetCustomAttribute<CallbackQueriesAttribute>(false)?.FunctionCode ?? "nokey_" + Random.Shared.Next(1, 10000000));

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("ICommand") == typeof(ICommand) && x.IsClass)
            .As<ICommand>()
            .SingleInstance()
            .Keyed<ICommand>(x => (x.GetCustomAttribute<CommandAttribute>(false)?.Name ?? "nokey_" + Random.Shared.Next(1, 10000000)) + "/*" + (int)(x.GetCustomAttribute<CommandAttribute>(false)?.ChatType ?? ChatType.Private));

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("IKeyboardButtonMessage") == typeof(IKeyboardButtonMessage) && x.IsClass)
            .As<IKeyboardButtonMessage>()
            .SingleInstance()
            .Keyed<IKeyboardButtonMessage>(x => x.GetCustomAttribute<KeyboardButtonMessageAttribute>(false)?.Text ?? "nokey_" + Random.Shared.Next(1, 10000000));

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("IProcess") == typeof(IProcess) && x.IsClass)
            .As<IProcess>()
            .SingleInstance()
            .Keyed<IProcess>(x =>
            x.GetCustomAttribute<ProcessAttribute>(false)?.Key ?? "nokey_" + Random.Shared.Next(1, 10000000)
            //(x.GetCustomAttribute<ProcessAttribute>(false).StepId.ToString() + "_"
            //+ x.GetCustomAttribute<ProcessAttribute>(false).StepIndexId.ToString()).ToString()
            );
    }

    public IContainer BaseContainer { get; }

    public IContainer CommandContainer { get; }

    public IContainer ProcessContainer { get; }

    public IContainer VoiceMessageContainer { get; }

    public IContainer KeyboardButtonMessageContainer { get; }

    public IContainer CallbackQueryContainer { get; }

    public IContainer VideoMessageContainer { get; }

    public IContainer MQTTContainer { get; }

    public IContainer GetCallbackQueryContainer()
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("ICallbackQuery") == typeof(ICallbackQuery) && x.IsClass)
            .As<ICallbackQuery>()
            .SingleInstance()
            .Keyed<ICallbackQuery>(x => x.GetCustomAttribute<CallbackQueriesAttribute>(false)?.FunctionCode ?? "nokey_" + Random.Shared.Next(1, 10000000));

        return builder.Build();
    }

    public IContainer GetCommandContainer()
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("ICommand") == typeof(ICommand) && x.IsClass)
            .As<ICommand>()
            .SingleInstance()
            .Keyed<ICommand>(x => (x.GetCustomAttribute<CommandAttribute>(false)?.Name ?? "nokey_" + Random.Shared.Next(1, 10000000)) + "/*" + (int)(x.GetCustomAttribute<CommandAttribute>(false)?.ChatType ?? ChatType.Private));

        return builder.Build();
    }

    public IContainer GetControllerContainer()
    {
        ContainerBuilder builder = new ContainerBuilder();

        //builder.RegisterType<MessageController>();
        //builder.RegisterType<BotConfiguration>().As<IBotConfiguration>();
        //builder.RegisterType<AutofacDependencyInjection>().As<IDependencyInjection>();
        //builder.RegisterType<TelegramBotManager>().As<IBotService>();
        //builder.RegisterType<EfBotRepository>().As<IBotDal>();
        //builder.RegisterType<UserAuthManager>().As<IUserAuthService>();
        //builder.RegisterType<EfUserRepository>().As<IUserDal>();
        //builder.RegisterType<EfUserStepRepository>().As<IUserStepDal>();
        //builder.RegisterType<EfStepDataRepository>().As<IStepDataDal>();

        //builder.RegisterType<UpdateHandle>().As<IHandle>();

        return builder.Build();
    }

    public IContainer GetKeyboardButtonMessageContainer()
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("IKeyboardButtonMessage") == typeof(IKeyboardButtonMessage) && x.IsClass)
            .As<IKeyboardButtonMessage>()
            .SingleInstance()
            .Keyed<IKeyboardButtonMessage>(x => x.GetCustomAttribute<KeyboardButtonMessageAttribute>(false)?.Text ?? "nokey_" + Random.Shared.Next(1, 10000000));

        return builder.Build();
    }

    public IContainer GetProcessContainer()
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("IProcess") == typeof(IProcess) && x.IsClass)
            .As<IProcess>()
            .SingleInstance()
            .Keyed<IProcess>(x =>
            x.GetCustomAttribute<ProcessAttribute>(false)?.Key ?? "nokey_" + Random.Shared.Next(1, 10000000)
            //(x.GetCustomAttribute<ProcessAttribute>(false).StepId.ToString() + "_"
            //+ x.GetCustomAttribute<ProcessAttribute>(false).StepIndexId.ToString()).ToString()
            );

        return builder.Build();
    }

    public ContainerBuilder GetBasicRegisters(ContainerBuilder builder = null!)
    {
        if (builder == null)
            builder = new ContainerBuilder();
        builder.RegisterInstance(_environment).As<IWebHostEnvironment>().SingleInstance();
        builder.RegisterInstance(_configuration).As<IConfiguration>();

        builder.RegisterType<BotConfiguration.BotConfiguration>().As<IBotConfiguration>().SingleInstance();

        builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
        builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();

        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
        //builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();

        builder.RegisterType<EfUserStepDal>().As<IUserStepDal>().SingleInstance();
        builder.RegisterType<UserStepManager>().As<IUserStepService>().SingleInstance();

        builder.RegisterType<EfBotDal>().As<IBotDal>().SingleInstance();
        builder.RegisterType<BotManager>().As<IBotService>().SingleInstance();

        builder.RegisterType<EfGroupDal>().As<IGroupDal>().SingleInstance();
        builder.RegisterType<GroupManager>().As<IGroupService>().SingleInstance();

        builder.RegisterType<EfMQTTCredentialDal>().As<IMQTTCredentialDal>().SingleInstance();
        builder.RegisterType<MQTTCredentialManager>().As<IMQTTCredentialService>().SingleInstance();

        builder.RegisterType<EfLanguageDal>().As<ILanguageDal>().SingleInstance();

        //builder.RegisterType<InlineKeyboardManager>().As<IInlineKeyboardService>().SingleInstance();
        //builder.RegisterType<TrackRequestManager>().As<ITrackRequestService>().SingleInstance();
        //builder.RegisterType<EfTrackRepository>().As<ITrackDal>().SingleInstance();

        //builder.RegisterType<OneServiceShazamRequestManager>().As<IShazamRequestService>().SingleInstance();
        //builder.RegisterType<OneServiceShazamApiManager>().As<IShazamApiService>().SingleInstance();


        //builder.RegisterType<YoutubeRequestManager>().As<IYoutubeRequestService>().SingleInstance();
        //builder.RegisterType<OneServiceYoutubeApiManager>().As<IYoutubeApiService>().SingleInstance();


        //builder.RegisterType<BotConfiguration>().As<IBotConfiguration>().SingleInstance();
        //builder.RegisterType<UserAuthManager>().As<IUserAuthService>().SingleInstance();
        //builder.RegisterType<EfUserRepository>().As<IUserDal>().SingleInstance();
        //builder.RegisterType<EfUserStepRepository>().As<IUserStepDal>().SingleInstance();
        //builder.RegisterType<EfStepDataRepository>().As<IStepDataDal>().SingleInstance();
        //builder.RegisterType<PageManager>().As<IPageService>().SingleInstance();
        //builder.RegisterType<TelegramBotManager>().As<IBotService>().SingleInstance();
        //builder.RegisterType<EfBotRepository>().As<IBotDal>().SingleInstance();
        //builder.RegisterType<UserStepManager>().As<IUserStepService>().SingleInstance();
        //builder.RegisterType<SpotifyServiceManager>().As<ISpotifyServiceService>().SingleInstance();
        //builder.RegisterType<RequestDbProcessManager>().As<IRequestDbProcessService>().SingleInstance();
        //builder.RegisterType<EfRequestRepository>().As<IRequestDal>().SingleInstance();
        //builder.RegisterType<StepDataManager>().As<IUserStepDataService>().SingleInstance();
        //builder.RegisterType<EfApiUrlRepository>().As<IApiUrlDal>().SingleInstance();
        //builder.RegisterType<ChannelManager>().As<IChannelService>().SingleInstance();
        //builder.RegisterType<EfChannelRepository>().As<IChannelDal>().SingleInstance();
        //builder.RegisterType<EfPostRepository>().As<IPostDal>().SingleInstance();
        //builder.RegisterType<TelegramJoinValidator>().As<IJoinValidator>().SingleInstance();

        //builder.RegisterType<AutofacDependencyInjection>().As<IDependencyInjection>().SingleInstance();



        //builder.RegisterType<UpdateHandle>().As<IHandle>().SingleInstance();


        //builder.RegisterType<SpotifyRequestManager>().As<ISpotifyRequestService>().SingleInstance();
        //builder.RegisterType<OneServiceRadioJavanRequestManager>().As<ISpotifyAlternativeRequestService>().SingleInstance();
        //builder.RegisterType<OneServiceRadioJavanRequestManager>().As<IRadioJavanRequestSerivce>().SingleInstance();
        //builder.RegisterType<OneServiceRadioJavanApiManager>().As<IRadioJavanApiService>().SingleInstance();

        builder.RegisterType<JwtHelper<User, OperationClaim, UserOperationClaim>>().As<ITokenHelper<User, OperationClaim>>();

        var assembly = Assembly.GetExecutingAssembly();
        var telegramBotCoreAssembly = TelegramBotCoreAssembly.GetAssembly;
        var businessAssembly = BusinessAssembly.GetAssembly;

        //////////////Stackoverflow error core///////////////
        //builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
        //    .EnableInterfaceInterceptors(new ProxyGenerationOptions()
        //    {
        //        Selector = new AspectInterceptorSelector()
        //    }).SingleInstance();

        builder.RegisterAssemblyTypes(telegramBotCoreAssembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        builder.RegisterAssemblyTypes(businessAssembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        return builder;
    }

    public IContainer GetVideoMessageContainer()
    {
        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(_asm)
            .Where(x => x.GetInterface("IVideoMessage") == typeof(IVideoMessage) && x.IsClass)
            .As<IVideoMessage>()
            .SingleInstance()
            .Keyed<IVideoMessage>(x => x.GetCustomAttribute<VideoMessageAttribute>(false)?.Key ?? "nokey_" + Random.Shared.Next(1, 10000000));

        return builder.Build();
    }

    public IContainer GetVoiceMessageContainer()
    {
        Assembly asm = TelegramBotCoreAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("IVoiceMessage") == typeof(IVoiceMessage) && x.IsClass)
            .As<IVoiceMessage>()
            .Keyed<IVoiceMessage>(x => x.GetCustomAttribute<VoiceMessageAttribute>(false)?.Key ?? "nokey_" + Random.Shared.Next(1, 10000000));

        return builder.Build();
    }

    public IContainer GetMQTTContainer()
    {
        Assembly asm = MQTTAssembly.GetAssembly;

        ContainerBuilder builder = GetBasicRegisters();

        builder.RegisterAssemblyTypes(asm)
            .Where(x => x.GetInterface("ITopic") == typeof(ITopic) && x.IsClass)
            .As<ITopic>()
            .SingleInstance()
            .Keyed<ITopic>(x =>
            x.GetCustomAttribute<TopicAttribute>(false)?.Topic ?? ""
            //(x.GetCustomAttribute<ProcessAttribute>(false).StepId.ToString() + "_"
            //+ x.GetCustomAttribute<ProcessAttribute>(false).StepIndexId.ToString()).ToString()
            );

        return builder.Build();
    }
}
