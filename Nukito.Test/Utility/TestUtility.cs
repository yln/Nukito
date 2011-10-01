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
      IResolver resolver = new NukitoFactory(settings ?? new NukitoSettings()).NewResolver();
      var command = new NukitoFactCommand(methodInfo, resolver);

      return () => command.Execute(method.Target);
    }
  }
}