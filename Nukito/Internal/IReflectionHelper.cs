using System.Reflection;

namespace Nukito.Internal
{
  public interface IReflectionHelper
  {
    object InvokeMethod (MethodInfo method, object instance, object[] arguments);
    object InvokeConstructor (ConstructorInfo constructor, object[] arguments);
  }
}