using Business.Abstract;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace Business.LangService.Abstract;

public class MessageAspectAttribute : MethodInterception
{
    IUserService _userService;

    public MessageAspectAttribute()
    {
        _userService = ServiceTool.ServiceProvider.GetService<IUserService>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var update = (Update)invocation.Arguments.First(x => x.GetType() == typeof(Update));
        //invocation.Arguments[2] =_userService.GetUserLang(update.)
    }
}
