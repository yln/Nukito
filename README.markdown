### Why ?
Do you like your unit tests concise and readable?
Do you enjoy using [xUnit][xunit] and [Moq][moq]?
Then Nukito is for you!

### What ?
Nukito lets you declare the SUT and mocks you need for testing as parameters of your test methods.
It creates these objects for you which frees you from writing repetitive object creation and mock setup / verification code.
It is similar to [Jukito][jukito] for Java.

### Where ?
Get it from the [NuGet gallary][nuget].

### Show me !
The following compares a standard xUnit test with a Nukito unit test.

This is a basic example for an unit test using xUnit, Moq and Fluent Assertions.

```c#
[Fact]
public void FightWithoutNukito ()
{
  // Arrange
  var weapon = new Mock<IWeapon>();
  var samurai = new Samurai (weapon.Object);
  weapon.Setup (w => w.Name).Returns ("katana");

  // Act
  string result = samurai.Fight();

  // Assert
  result.Should().Be ("Samurai fights with katana");
  weapon.VerifyAll(); // Verifies invocation of getter (IWeapon.Name)
}
```

Adding Nukito to the mix results in the following equivalent test.
Note that Nukito verifies all setup expectations for requested mocks by default.

```c#
[NukitoFact]
public void FightWithNukito (Samurai samurai, Mock<IWeapon> weapon)
{
  // Arrange
  weapon.Setup (w => w.Name).Returns ("nunchaku");

  // Act
  string result = samurai.Fight();

  // Assert
  result.Should().Be ("Samurai fights with nunchaku");
}
```

### Examples

* [A basic example](https://github.com/yln/Nukito/blob/master/Nukito.Example/BasicExample.cs)
* [Requesting mocks via the constructor](https://github.com/yln/Nukito/blob/master/Nukito.Example/ConstructorExample.cs)
* [Working with contexts](https://github.com/yln/Nukito/blob/master/Nukito.Example/ContextExample.cs)
* [Configuring mock settings](https://github.com/yln/Nukito/blob/master/Nukito.Example/SettingsExample.cs)
* [Requesting the MockRepository](https://github.com/yln/Nukito/blob/master/Nukito.Example/MockRepositoryExample.cs)

All the examples and the test domain can be found [here][examples].

### Feedback
Did you encounter a bug or miss a feature? Create a new [issue][issues].  
Do you want to give general feedback or help to spread the word?
Rate or review Nukito at the [NuGet gallary][nuget].


[xunit]:    http://xunit.codeplex.com
[moq]:      http://code.google.com/p/moq/wiki/QuickStart
[jukito]:   http://code.google.com/p/jukito
[nuget]:    http://nuget.org/List/Packages/Nukito
[issues]:   https://github.com/yln/Nukito/issues
[examples]: https://github.com/yln/Nukito/tree/master/Nukito.Example
