using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class MockBehaviorScenarios
  {
    [NukitoFact]
    public void DefaultMockBehavior(Mock<IA> mock)
    {
      // Assert (Default -> Loose)
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
    }

    [NukitoFact]
    [NukitoSettings(MockBehavior = MockBehavior.Strict)]
    public void MockBehaviorCanBeConfigured(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
    }
  }
}