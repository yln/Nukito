using Moq;
using Nukito.Internal;

namespace Nukito.Test.Scenario
{
  public class VerifyMockScenarios
  {
    [NukitoFact]
    public void DefaultIsVerifyAll(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething());

      // Act
      mock.Object.DoSomething();

      // Assert
      // This test should not throw an exception; all implicit expectations are met
    }

    [NukitoFact(MockVerification = MockVerification.Marked)]
    public void VerifyMarked(Mock<IB> mock)
    {
      // Assert
      mock.Setup(b => b.DoSomething()).Verifiable();

      // Act
      mock.Object.DoSomething();

      // Assert
      // This test should not throw an exception; all marked expectations are met
    }

    [NukitoFact(MockVerification = MockVerification.None)]
    public void VerificationForSpecifiedExpectations(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething()).Verifiable();

      // Act
      // Expectations are not fulfilled

      // Assert
      // This test should not throw an exception; expectations are ignored
    }

    // TODO: add failing tests for cases when expectationsa are not met
  }
}