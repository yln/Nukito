using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  [VisibleForTesting]
  public class MaxArgumentsPublicConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      ConstructorInfo[] constructors = type.GetConstructors();
      if (constructors.Length == 0)
      {
        return null;
      }
      int maxParameters = constructors.Max(ci => ci.GetParameters().Length);

      return constructors.SingleOrDefaultForAny(ci => ci.GetParameters().Length == maxParameters);
    }

    public string StrategyDescription
    {
      get { return "Public constructor with most arguments"; }
    }
  }
}