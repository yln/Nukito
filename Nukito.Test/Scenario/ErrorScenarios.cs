using System;
using System.Reflection;
using FluentAssertions;
using Moq;
using Nukito.Internal;
using Xunit.Sdk;

namespace Nukito.Test.Scenario
{
  public class ErrorScenarios
  {
    private static readonly MoqResolver _Resolver = new MoqResolver();

    private void ShouldThrowNukitoExceptionForMethodParameterOfType<T>(string exceptionMessage)
    {
      // Arrange
      MethodInfo methodInfo = new Action<T>(RequestInvalidParameter).Method;
      var factCommand = new NukitoFactCommand(Reflector.Wrap(methodInfo), _Resolver);

      // Act
      Action execution = () => factCommand.Execute(this);

      // Assert
      execution.ShouldThrow<NukitoException>().WithMessage(exceptionMessage);
    }

    private void RequestInvalidParameter<T>(T invalidParameter)
    {
      // Do nothing
    }

    [NukitoFact]
    public void RequestInvalidMockType()
    {
      ShouldThrowNukitoExceptionForMethodParameterOfType<Mock>(
        "The generic version Mock<T> must be used in place of Mock");
    }

    [NukitoFact]
    public void RequestMockForConcreteClass()
    {
      ShouldThrowNukitoExceptionForMethodParameterOfType<Mock<A>>(
        "Can not create mock for type Nukito.Test.Scenario.A");
    }
  }
}