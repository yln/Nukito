using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  public class MockSettingsScenarios
  {
    [NukitoFact]
    public void DefaultMockBehavior(Mock<IA> mock)
    {
      // Assert (Default -> Loose)
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
    }

    [NukitoFact, MockSettings (Behavior = MockBehavior.Strict)]
    public void MockBehaviorCanBeConfigured(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
    }

    [NukitoFact]
    public void DefaultCallBase(Mock<IA> mock)
    {
      // Assert
      mock.CallBase.Should().BeFalse();
    }

    [NukitoFact, MockSettings (CallBase = true)]
    public void CallBaseCanBeConfigured(Mock<IA> mock)
    {
      // Assert
      mock.CallBase.Should().BeTrue();
    }

    [NukitoFact]
    public void DefaultDefaultValue(Mock<IA> mock)
    {
      // Assert
      mock.DefaultValue.Should().Be(DefaultValue.Mock);
    }

    [NukitoFact, MockSettings (DefaultValue = DefaultValue.Empty)]
    public void DefaultValueCanBeConfigured(Mock<IA> mock)
    {
      // Assert
      mock.DefaultValue.Should().Be(DefaultValue.Empty);
    }
  }
}