using FluentAssertions;
using Moq;
using Nukito.Internal;

namespace Nukito.Test.Scenario
{
  // --- Default Settings ---
  // MockBehavior -> Loose (and also 'Default')
  // MockVerification -> All
  [NukitoSettings(MockBehavior = MockBehavior.Strict, MockVerification = MockVerification.Marked)]
  public class ClassWideSettingsScenarios
  {
    [NukitoFact]
    public void ClassWideMockBehavior(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
    }

    [NukitoFact(MockBehavior = MockBehavior.Loose)]
    public void ReConfiguredMockBehavior(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
    }

    [NukitoFact]
    public void ClassWideMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething());

      // Assert: no exception
      // Implicit expectations (default overwritten by class-wide) are not fullfilled
    }

    [NukitoFact(MockVerification = MockVerification.None)]
    public void ReConfiguredMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething()).Verifiable();

      // Assert: no exception
      // Marked expectations (class-wide overwritten by method) are not fullfilled
    }
  }
}