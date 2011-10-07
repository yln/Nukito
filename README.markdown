### Why?
Do you like your unit tests concise and readable?
Do you enjoy using [xUnit][xunit] and [Moq][moq]?
Then Nukito is for you!

### What?
Nukito lets you declare the SUT and mocks you need for testing as parameters of your test methods.
It creates these objects for you which frees you from writing repetitive object creation and mock setup / verification code.
It is similar to [Jukito][jukito] for Java.

### Where?
Get it from the [NuGet gallary][nuget].

### Show me!
The following compares a standard xUnit test with a Nukito unit test.

This is a basic example for an unit test using xUnit, Moq and fluent assertions.

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
      weapon.VerifyAll();  // Verifies invocation of getter (IWeapon.Name)
    }


Adding Nukito to the mix results in the following equivalent test.
Note that Nukito verifies all setup expectations for requested mocks by default.

    [NukitoFact]
    public void FightWithNukito(Samurai samurai, Mock<IWeapon> weapon)
    {
      // Arrange
      weapon.Setup(w => w.Name).Returns("nunchaku");

      // Act
      string result = samurai.Fight();

      // Assert
      result.Should().Be("Samurai fights with nunchaku");
    }


### Examples

* [A basic example](http://github.com/yln/Nukito/blob/master/Nukito.Test/Example/BasicExample.cs)
* [Configuring mock settings](http://github.com/yln/Nukito/blob/master/Nukito.Test/Example/SettingsExample.cs)

All the examples and the test domain can be found [here][examples].


[xunit]: http://xunit.codeplex.com
[moq]: http://code.google.com/p/moq/wiki/QuickStart
[jukito]: http://code.google.com/p/jukito
[nuget]: http://nuget.org/List/Packages/Nukito
[wiki]: http://github.com/yln/Nukito/wiki
[examples]: http://github.com/yln/Nukito/tree/master/Nukito.Test/Example
