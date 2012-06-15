﻿using FluentAssertions;
using Moq;

namespace Nukito.Example
{
  public class ConstructorExample
  {
    private readonly Samurai _samurai;

    // With xUnit the constructor can be used for common setup.
    public ConstructorExample(Samurai samurai, Mock<IWeapon> weapon)
    {
      _samurai = samurai;

      weapon.Setup(w => w.Name).Returns("katana");
    }

    [NukitoFact]
    public void FightWithConstructorForCommonSetup()
    {
      // Act
      string result = _samurai.Fight();

      // Assert
      result.Should ().Be ("Samurai fights with katana");
    }

    // Do not verify expectations from the constructor.
    [NukitoFact, NukitoSettings(MockVerification = MockVerification.Marked)]
    public void FightAndIgnoreCommonSetup()
    {
      // Do something completely different ...
    }
  }
}