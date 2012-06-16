using System;
using System.Diagnostics;
using System.Reflection;

namespace Nukito.Internal
{
  // TODO: Consider creating delegates to speedup reflection.
  public class ReflectionHelper : IReflectionHelper
  {

    public object InvokeMethod(MethodInfo method, object instance, object[] arguments)
    {
      return UnwrapTargetInvocationException(() => method.Invoke(instance, arguments));
    }

    public object InvokeGenericMethod(MethodInfo genericMethodDefinition, Type[] genericArguments, object instance, object[] arguments)
    {
      Debug.Assert(genericMethodDefinition.IsGenericMethodDefinition);

      var method = genericMethodDefinition.MakeGenericMethod(genericArguments);
      return InvokeMethod(method, instance, arguments);
    }

    public object InvokeConstructor(ConstructorInfo constructor, object[] arguments)
    {
      return UnwrapTargetInvocationException(() => constructor.Invoke(arguments));
    }

    private object UnwrapTargetInvocationException(Func<object> invocation)
    {
      try
      {
        return invocation();
      }
      catch (TargetInvocationException ex)
      {
        throw new NukitoException(ex.InnerException.Message, ex.InnerException);
      }
    }
  }
}