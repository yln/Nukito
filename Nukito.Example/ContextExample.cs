using FluentAssertions;

namespace Nukito.Example
{
  // [Ctx ("...")] can be used to assign a parameter to a context.
  // Within a context all instances are singletons.
  // All parameters without a [Ctx] attribute are placed in the default context.
  public class ContextExample
  {
    private readonly IWeapon _weapon1;
    private readonly IWeapon _weapon2;

    public ContextExample (IWeapon weapon1, [Ctx ("a")] IWeapon weapon2)
    {
      _weapon1 = weapon1;
      _weapon2 = weapon2;

      // Assert
      weapon1.Should().NotBeSameAs (weapon2);
    }

    [NukitoFact]
    public void FightWithDefaultContext (IWeapon weapon1, IWeapon weapon2)
    {
      // Assert
      weapon1.Should().BeSameAs (weapon2);
      weapon1.Should().BeSameAs (_weapon1);
      weapon1.Should().NotBeSameAs (_weapon2);
    }

    [NukitoFact]
    public void FightWithCustomContext (
      [Ctx ("a")] Samurai samurai1, [Ctx ("a")] IWeapon weapon1,
      [Ctx ("b")] Samurai samurai2, [Ctx ("b")] IWeapon weapon2)
    {
      // Assert
      samurai1.Should().NotBeSameAs (samurai2);
      samurai1.Weapon.Should().Be (weapon1);
      samurai2.Weapon.Should().Be (weapon2);
    }
  }
}