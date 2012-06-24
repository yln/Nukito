using FluentAssertions;
using Moq;

namespace Nukito.Example
{
  public class ConstructorExample
  {
    private readonly Samurai _samurai;

    // With xUnit the constructor can be used for common setup.
    public ConstructorExample (Samurai samurai, Mock<IWeapon> weapon)
    {
      _samurai = samurai;

      // Arrange
      weapon.Setup (w => w.Name).Returns ("katana");
    }

    [NukitoFact]
    public void FightWithConstructorForCommonSetup ()
    {
      // Act
      string result = _samurai.Fight();

      // Assert
      result.Should ().Be ("Samurai fights with katana");
    }

    // Do not verify expectations from the constructor.
    [NukitoFact, MockSettings (Verification = MockVerification.Marked)]
    public void FightWithoutCommonSetup ()
    {
      // Do something completely different ...
    }
  }
}