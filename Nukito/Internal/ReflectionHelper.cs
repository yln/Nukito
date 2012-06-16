using System.Reflection;

namespace Nukito.Internal
{
  // TODO: Consider creating delegate to speedup reflection.
  public class ReflectionHelper : IReflectionHelper
  {

    public object InvokeMethod(MethodInfo method, object instance, object[] arguments)
    {
      return method.Invoke (instance, arguments);
    }

    public object InvokeConstructor(ConstructorInfo constructor, object[] arguments)
    {
      return constructor.Invoke (arguments);
    }
  }
}