## Why?
Do you like your unit tests concise and readable?
Do you enjoy using [xUnit][xunit] and [Moq][moq]?
Then Nukito is for you!

## What?
Nukito provides mocks for the method parameters of your test methods.
It frees you from writing repetitive mock setup and verification code.
It is similar to [Jukito][jukito] for Java.

## Where?
Get it from the [NuGet gallary][nuget].

## Show me!
The following is a basic example for a Nukito unit test. Check out the [Wiki][wiki] for more examples.

  public class Samurai : IWarrior
  {
    public IWeapon Weapon { get; private set; }

    public Samurai(IWeapon weapon)
    {
      Weapon = weapon;
    }

    public string Fight()
    {
      return "Samurai fights with " + Weapon.Name;
    }
  }

      // This is a simple example for a unit test 
      // using xUnit, Moq and fluent assertions.
      [Fact]
      public void TestFightWithXunit()
      {
        // Arrange
        var weapon = new Mock<IWeapon>();
        var samurai = new Samurai(weapon.Object);
        weapon.Setup(w => w.Name).Returns("katana");

        // Act
        string result = samurai.Fight();

        // Assert
        result.Should().Be("Samurai fights with katana");
        weapon.VerifyAll(); // Verifies getter (IWeapon.Name)
      }

      // Adding Nukito to the mix results in the following
      // test which is equivalent to the one above.
      [NukitoFact]
      public void TestFightWithNuktio(Samurai samurai, Mock<IWeapon> weapon)
      {
        // Arrange
        weapon.Setup(w => w.Name).Returns("nunchaku");

        // Act
        string result = samurai.Fight();

        // Assert
        result.Should().Be("Samurai fights with nunchaku");
        // Expectations for requested mocks are verified by default.
      }


[xunit]: http://xunit.codeplex.com
[moq]: http://code.google.com/p/moq/wiki/QuickStart
[jukito]: http://code.google.com/p/jukito
[nuget]: http://nuget.org/List/Packages/Nukito
[wiki]: http://github.com/yln/Nukito/wiki
