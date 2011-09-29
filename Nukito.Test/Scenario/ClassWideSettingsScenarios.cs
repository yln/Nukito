using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  // --- Default Settings ---
  // MockBehavior -> Default (Loose)
  [NukitoSettings(MockBehavior = MockBehavior.Strict)]
  public class ClassWideSettingsScenarios
  {
    [NukitoFact]
    public void ClassWideMockBehavior(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
    }

    [NukitoFact]
    // TODO: add settings to override class wide settings
    public void ReConfiguredMockBehavior(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
    }
  }
}