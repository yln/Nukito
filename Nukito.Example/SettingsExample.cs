using FluentAssertions;
using Moq;

namespace Nukito.Example
{
  // Mock settings may be configured on class level.
  // The following settings are the defaults.
  [MockSettings(
    Behavior = MockBehavior.Loose,
    CallBase = false,
    DefaultValue = DefaultValue.Mock,
    Verification = MockVerification.All)]
  public class SettingsExample
  {
    [NukitoFact]
    public void MockSettingsDefaults(Mock<IWarrior> warrior)
    {
      // Arrange
      warrior.Setup(w => w.Fight()).Returns("Zonk!");

      // Act
      string result = warrior.Object.Fight();
      // Not calling 'Fight()' would cause this test to fail.
      //   Moq.MockException : The following setups were not matched:
      //   IWarrior w => w.Fight()

      // Assert
      warrior.Behavior.Should().Be(MockBehavior.Loose);
    }


    // Mock settings can be configured on method and class level.
    [NukitoFact]
    [MockSettings(Behavior = MockBehavior.Strict, Verification = MockVerification.None)]
    public void MockSettingsCanBeConfigured(Mock<IWarrior> warrior)
    {
      // Arrange
      warrior.Setup(w => w.Fight()).Returns("Splat!");

      // Act
      // This test still passes although we are not calling 'Fight()'.

      // Assert
      warrior.Behavior.Should().Be(MockBehavior.Strict);
    }
  }
}