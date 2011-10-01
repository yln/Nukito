using System;
using FluentAssertions;
using Moq;
using Nukito.Test.Utility;

namespace Nukito.Test.Scenario
{
  public class ErrorScenarios
  {
    private void RequestInvalidParameter<T>(T invalidParameter)
    {
      // Do nothing
    }

    private Action GetTest<T>()
    {
      return TestUtility.GetTest<T>(RequestInvalidParameter);
    }


    [NukitoFact]
    public void RequestInvalidMockType()
    {
      // Act + Assert
      GetTest<Mock>()
        .ShouldThrow<NukitoException>()
        .WithMessage("The generic version Mock<T> must be used in place of Mock");
    }

    [NukitoFact]
    public void RequestMockForConcreteClass()
    {
      // Act + Assert
      GetTest<Mock<A>>()
        .ShouldThrow<NukitoException>()
        .WithMessage("Can not create mock for type Nukito.Test.Scenario.A");
    }
  }
}