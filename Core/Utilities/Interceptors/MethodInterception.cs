using Castle.DynamicProxy;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

namespace Core.Utilities.Interceptors;

public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    protected virtual void OnBefore(IInvocation invocation) { }
    protected virtual void OnAfter(IInvocation invocation) { }
    protected virtual void OnException(IInvocation invocation, System.Exception e) { }
    protected virtual void OnSuccess(IInvocation invocation) { }
    public override void Intercept(IInvocation invocation)
    {
        var isSuccess = true;
        OnBefore(invocation);
        try
        {
            invocation.Proceed();
        }
        catch (Exception e)
        {
            isSuccess = false;
            OnException(invocation, e);
            if (invocation.Method.ReturnType == typeof(IResult))
            {
                invocation.ReturnValue = new ErrorResult(message: $"message: {e.Message}\n\nInnerException: {e.InnerException}");
            }
            //else if (invocation.Method.ReturnType.GetInterfaces().Contains(typeof(IResult)))
            //{
            //    Type type = invocation.Method.ReturnType;
            //    ////var protoType = type.GetProperty("Data")?.GetType();
            //    //var genericTypeArgument = type.GetGenericArguments();
            //    //var instance = Activator.CreateInstance(typeof(ErrorDataResult<object>);

            //    //var properties = type.GetProperties().ToList();
            //    //properties.FirstOrDefault(x => x.Name == "Success")?.SetValue(instance, false);
            //    //properties.FirstOrDefault(x => x.Name == "Message")?.SetValue(instance, $"message: {e.Message}\n\nInnerException: {e.InnerException}");
            //    invocation.ReturnValue = new { Success = false, Message = $"message: {e.Message}\n\nInnerException: {e.InnerException}" };
            //    invocation.ReturnValue = JsonConvert.DeserializeObject("{ \"success\" : false, \"message\" : \"message: " + e.Message + "\\n\\nInnerException: "+ e.InnerException + "\" }", type);
            //    //new ErrorDataResult<Type>(message: $"message: {e.Message}\n\nInnerException: {e.InnerException}");
            //    //(message: $"message: {e.Message}\n\nInnerException: {e.InnerException}")
            //}
            else
            {
                throw;
            }
        }
        finally
        {
            if (isSuccess)
            {
                OnSuccess(invocation);
            }
        }
        OnAfter(invocation);
    }
}
