using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  // --- Default Settings ---
  // Behavior     -> Loose (and also 'Default')
  // CallBase     -> false
  // DefaultValue -> Mock
  // Verification -> All
  [MockSettings(
    Behavior = MockBehavior.Strict,
    CallBase = true,
    DefaultValue = DefaultValue.Empty,
    Verification = MockVerification.Marked)]
  public class ClassWideSettingsScenarios
  {
    [MockSettings (Behavior = MockBehavior.Loose, CallBase = false, DefaultValue = DefaultValue.Mock)]
    public ClassWideSettingsScenarios(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should ().Be (MockBehavior.Default).And.Be (MockBehavior.Loose);
      mock.CallBase.Should ().BeFalse ();
      mock.DefaultValue.Should ().Be (DefaultValue.Mock);
    }

    [NukitoFact]
    public void ClassWideMockSettings(Mock<IB> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
      mock.CallBase.Should().BeTrue();
      mock.DefaultValue.Should().Be(DefaultValue.Empty);
    }

    [NukitoFact]
    [MockSettings (Behavior = MockBehavior.Loose, DefaultValue = DefaultValue.Mock, CallBase = false)]
    public void ReConfiguredMockSettings(Mock<IB> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
      mock.DefaultValue.Should ().Be (DefaultValue.Mock);
      mock.CallBase.Should().BeFalse();
    }

    [NukitoFact]
    public void ClassWideMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething());

      // Assert: no exception
      // Implicit expectations (default overwritten by class-wide) are not fullfilled
    }

    [NukitoFact, MockSettings (Verification = MockVerification.None)]
    public void ReConfiguredMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething()).Verifiable();

      // Assert: no exception
      // Marked expectations (class-wide overwritten by method) are not fullfilled
    }
  }
}