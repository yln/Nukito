using FluentAssertions;
using Moq;
using Xunit;

namespace Nukito.Test.Example
{
  public class BasicExample
  {
    // This is a simple example for an unit test 
    // using xUnit, Moq and fluent assertions.
    [Fact]
    public void FightWithoutNukito()
    {
      // Arrange
      var weapon = new Mock<IWeapon>();
      var samurai = new Samurai(weapon.Object);
      weapon.Setup(w => w.Name).Returns("katana");

      // Act
      string result = samurai.Fight();

      // Assert
      result.Should().Be("Samurai fights with katana");
      weapon.VerifyAll(); // Verifies invocation of getter (IWeapon.Name)
    }

    // Adding Nukito to the mix results in the following
    // test which is equivalent to the one above.
    [NukitoFact]
    public void FightWithNukito(Samurai samurai, Mock<IWeapon> weapon)
    {
      // Arrange
      weapon.Setup(w => w.Name).Returns("nunchaku");

      // Act
      string result = samurai.Fight();

      // Assert
      result.Should().Be("Samurai fights with nunchaku");
      // Expectations for requested mocks are verified by default.
    }
  }
}