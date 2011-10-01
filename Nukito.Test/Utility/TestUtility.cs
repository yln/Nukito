using System;
using FluentAssertions;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Utility
{
  public static class TestUtility
  {
    public static void ShouldThrow<T>(Delegate method, string exceptionMessage)
      where T : Exception
    {
      ITestCommand testCommand = GetTestCommand(method);
      Action execution = () => testCommand.Execute(method.Target);
      execution.ShouldThrow<T>().WithMessage(exceptionMessage);
    }

    private static ITestCommand GetTestCommand(Delegate method)
    {
      return new NukitoFactCommand(Reflector.Wrap(method.Method), new NukitoFactory().NewResolver());
    }
  }
}