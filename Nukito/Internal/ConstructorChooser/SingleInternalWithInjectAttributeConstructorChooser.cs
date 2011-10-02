using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class SingleInternalWithInjectAttributeConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      return type.GetInternalConstructors()
        .SingleOrDefaultForAny(ci => ci.GetCustomAttributes(false).Any(
          a => a.GetType().Name.ToLower().Contains("inject")));
    }

    public string StrategyDescription
    {
      get { return "Single internal constructor with 'Inject' attribute"; }
    }
  }
}