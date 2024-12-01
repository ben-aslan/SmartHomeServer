using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace TelegramBotCore.Aspects.Localization;

public class Localization : MethodInterception
{
    protected override void OnBefore(IInvocation invocation)
    {
        invocation.TargetType.GetField("");
    }
}
