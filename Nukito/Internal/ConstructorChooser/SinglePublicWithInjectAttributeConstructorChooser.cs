using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  [VisibleForTesting]
  public class SinglePublicWithInjectAttributeConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      return type.GetConstructors()
        .SingleOrDefaultForAny(ci => ci.GetCustomAttributes(false).Any(
          a => a.GetType().Name.ToLower().Contains("inject")));
    }

    public string StrategyDescription
    {
      get { return "Single public constructor with 'Inject' attribute"; }
    }
  }
}