using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Messages;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Core.Aspects.Autofac.Performance;

public class PerformanceAspect : MethodInterception
{
    private int _interval;
    private Stopwatch _stopwatch;
    private LoggerServiceBase _loggerServiceBase;
    private bool _logging;

    public PerformanceAspect(int interval, bool logging = true, Type loggerService = null!)
    {
        _interval = interval;
        _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>()!;

        if (loggerService == null)
        {
            loggerService = typeof(FileLogger);
        }
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService)!;
        _logging = logging;
    }


    protected override void OnBefore(IInvocation invocation)
    {
        _stopwatch.Start();
    }

    protected override void OnAfter(IInvocation invocation)
    {
        if (_stopwatch.Elapsed.TotalSeconds > _interval)
        {
            Debug.WriteLine($"Performance : {invocation!.Method!.DeclaringType!.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            if (_logging)
            {
                LogDetailWithException logDetailWithException = GetLogDetail(invocation);
                logDetailWithException.ExceptionMessage = $"Performance : {invocation!.Method!.DeclaringType!.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}";
                _loggerServiceBase.Error(logDetailWithException);
            }
        }
        _stopwatch.Reset();
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
    {
        var logParameters = new List<LogParameter>();

        for (int i = 0; i < invocation?.Arguments.Length; i++)
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name ?? "",
                Value = JsonConvert.SerializeObject(invocation.Arguments[i]),
                Type = invocation?.Arguments[i]?.GetType()?.Name
            });
        }

        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation?.Method?.Name ?? "",
            LogParameters = logParameters
        };

        return logDetailWithException;
    }
}
