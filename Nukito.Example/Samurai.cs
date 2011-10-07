namespace Nukito.Example
{
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
}