using System;
using Moq;
using Nukito.Test.Utility;

namespace Nukito.Test.Scenario
{
  public class ErrorScenarios
  {
    private void ShouldThrowNukitoExceptionForMethodParameterOfType<T>(string exceptionMessage)
    {
      TestUtility.ShouldThrow<NukitoException>(new Action<T>(RequestInvalidParameter), exceptionMessage);
    }

    private void RequestInvalidParameter<T>(T invalidParameter)
    {
      // Do nothing
    }

    [NukitoFact]
    public void RequestInvalidMockType()
    {
      // Act + Assert
      ShouldThrowNukitoExceptionForMethodParameterOfType<Mock>(
        "The generic version Mock<T> must be used in place of Mock");
    }

    [NukitoFact]
    public void RequestMockForConcreteClass()
    {
      // Act + Assert
      ShouldThrowNukitoExceptionForMethodParameterOfType<Mock<A>>(
        "Can not create mock for type Nukito.Test.Scenario.A");
    }
  }
}