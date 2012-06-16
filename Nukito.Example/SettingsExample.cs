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
    private readonly IWeapon _weapon;

    // Mock settings on constructors overwrite class level settings.
    // Usage of [MockSettings (Verification = ...)] is not allowed.
    //[MockSettings (Behavior = MockBehavior.Strict, Verification = MockVerification.None)]
    //public SettingsExample(Mock<IWeapon> weapon)
    //{
    // TODO: fix after 'injection' context has been implemented
    //  // Common setup
    //  weapon.Setup (w => w.Name).Returns("sword");
    //  _weapon = weapon.Object;
    //}

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


    // Mock setting on methods overwrite class level settings.
    [NukitoFact]
    [MockSettings (Behavior = MockBehavior.Strict, Verification = MockVerification.None)]
    public void MockSettingsCanBeConfigured(Mock<IWarrior> warrior)
    {
      // Arrange
      warrior.Setup(w => w.Fight()).Returns("Splat!");

      // Act
      // This test passes although 'Fight()' is not called.

      // Assert
      warrior.Behavior.Should().Be(MockBehavior.Strict);
    }
  }
}