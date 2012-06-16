using System;
using System.Linq;
using System.Reflection;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Utility
{
  internal static class TestUtility
  {
    public static Action GetTest<T> (
        Action<T> method,
        ConstructorInfo constructor = null,
        Internal.MockSettings settings = null,
        Internal.MockSettings constructorSettings = null)
    {
      IMethodInfo methodInfo = Reflector.Wrap(method.Method);
      constructor = constructor ?? method.Method.ReflectedType.GetConstructors().Single();
      settings = settings ?? new Internal.MockSettings();
      constructorSettings = constructorSettings ?? new Internal.MockSettings();

      ITestCommand command = NukitoFactory.CreateCommand (methodInfo, constructor, settings, constructorSettings);

      return () => command.Execute(null);
    }
  }
}