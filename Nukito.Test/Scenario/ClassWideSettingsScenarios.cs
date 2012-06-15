using FluentAssertions;
using Moq;

namespace Nukito.Test.Scenario
{
  // --- Default Settings ---
  // MockBehavior     -> Loose (and also 'Default')
  // CallBase         -> false
  // DefaultValue     -> Mock
  // MockVerification -> All
  [NukitoSettings(
    MockBehavior = MockBehavior.Strict,
    CallBase = true,
    DefaultValue = DefaultValue.Empty,
    MockVerification = MockVerification.Marked)]
  public class ClassWideSettingsScenarios
  {
    [NukitoFact]
    public void ClassWideMockSettings(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Strict);
      mock.CallBase.Should().BeTrue();
      mock.DefaultValue.Should().Be(DefaultValue.Empty);
    }

    [NukitoFact]
    [NukitoSettings (MockBehavior = MockBehavior.Loose, CallBase = false, DefaultValue = DefaultValue.Mock)]
    public void ReConfiguredMockSettings(Mock<IA> mock)
    {
      // Assert
      mock.Behavior.Should().Be(MockBehavior.Default).And.Be(MockBehavior.Loose);
      mock.CallBase.Should().BeFalse();
      mock.DefaultValue.Should().Be(DefaultValue.Mock);
    }

    [NukitoFact]
    public void ClassWideMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething());

      // Assert: no exception
      // Implicit expectations (default overwritten by class-wide) are not fullfilled
    }

    [NukitoFact, NukitoSettings (MockVerification = MockVerification.None)]
    public void ReConfiguredMockVerification(Mock<IB> mock)
    {
      // Arrange
      mock.Setup(b => b.DoSomething()).Verifiable();

      // Assert: no exception
      // Marked expectations (class-wide overwritten by method) are not fullfilled
    }
  }
}