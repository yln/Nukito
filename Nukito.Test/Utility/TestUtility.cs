using System;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Utility
{
  internal static class TestUtility
  {
    public static Action GetTest<T>(Action<T> method, INukitoSettings settings = null)
    {
      IMethodInfo methodInfo = Reflector.Wrap(method.Method);
      settings = settings ?? new NukitoSettings();
      ITestCommand command = NukitoFactory.CreateCommand(methodInfo, settings);

      return () => command.Execute(method.Target);
    }
  }
}