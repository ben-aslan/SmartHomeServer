using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Data;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction;

public class TransactionScopeAsyncAspect : MethodInterception
{
    public override async void Intercept(IInvocation invocation)
    {
        //using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, asyncFlowOption: TransactionScopeAsyncFlowOption.Enabled))
        //{
            try
            {
                invocation.Proceed();
                await ((Task)invocation.ReturnValue).ConfigureAwait(false);
                //transactionScope.Complete();
            }
            catch (System.Exception)
            {
                //transactionScope.Dispose();
                throw;
            }
        //}
    }
}
