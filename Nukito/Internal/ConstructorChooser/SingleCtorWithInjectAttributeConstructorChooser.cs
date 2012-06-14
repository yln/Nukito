using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class SingleCtorWithInjectAttributeConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      return type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(ci => ci.IsPublic || ci.IsFamilyOrAssembly || ci.IsAssembly)
        .SingleOrDefaultForAny(HasInjectAttribute);
    }

    private bool HasInjectAttribute(ConstructorInfo constructor)
    {
      return constructor.GetCustomAttributes(false).Any(a => a.GetType().Name.ToLower().Contains("inject"));
    }

    public string StrategyDescription
    {
      get { return "Single public, protected internal or internal constructor with [Inject] attribute"; }
    }
  }
}