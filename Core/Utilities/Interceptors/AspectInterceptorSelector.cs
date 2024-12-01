using Castle.DynamicProxy;
using System.Reflection;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Aspects.Autofac.Logging;

namespace Core.Utilities.Interceptors;

public class AspectInterceptorSelector : IInterceptorSelector
{
    public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
    {
        var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
            (true).ToList();
        var methodAttributes = type.GetMethod(method.Name)?
            .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
        classAttributes.AddRange(methodAttributes!);
        classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
        //classAttributes.Add(new LogAspect(typeof(FileLogger)));

        return classAttributes.OrderBy(x => x.Priority).ToArray();
    }
}
