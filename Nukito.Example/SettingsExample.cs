using FluentAssertions;
using Moq;

namespace Nukito.Example
{
  // Mock settings may be configured on class level.
  // The following settings are the defaults.
  [MockSettings (
    Behavior = MockBehavior.Loose,
    CallBase = false,
    DefaultValue = DefaultValue.Mock,
    Verification = MockVerification.All)]
  public class SettingsExample
  {
    private readonly Mock<IWeapon> _weapon;

    // Mock settings on constructors overwrite class level settings.
    // Usage of [MockSettings (Verification = ...)] has no effect.
    [MockSettings (Behavior = MockBehavior.Strict)]
    public SettingsExample (Mock<IWeapon> weapon)
    {
      weapon.Behavior.Should().Be (MockBehavior.Strict);
      // Common setup
      weapon.Setup (w => w.Name).Returns("sword");
      _weapon = weapon;
    }

    [NukitoFact]
    public void MockSettingsDefaults (Mock<IWarrior> warrior)
    {
      // Arrange
      warrior.Setup(w => w.Fight()).Returns("Zonk!");

      // Act
      var result1 = warrior.Object.Fight();
      var result2 = _weapon.Object.Name;

      // Not calling Fight() on warrior mock or not accessing 'Name'
      // on weapon mock would cause this test to fail.
      //   Moq.MockException : The following setups were not matched:
      //   IWarrior w => w.Fight()

      // Assert
      warrior.Behavior.Should().Be (MockBehavior.Loose);
    }

    // Mock setting on methods overwrite class level settings.
    [NukitoFact]
    [MockSettings (Behavior = MockBehavior.Strict, Verification = MockVerification.None)]
    public void MockSettingsCanBeConfigured (Mock<IWarrior> warrior)
    {
      // Arrange
      warrior.Setup(w => w.Fight()).Returns ("Splat!");

      // Act
      // This test passes although 'Fight()' is not called.

      // Assert
      warrior.Behavior.Should().Be (MockBehavior.Strict);
    }
  }
}