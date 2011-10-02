using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class MaxArgumentsPublicConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      ConstructorInfo[] constructors = type.GetConstructors();
      int maxParameters = constructors.Max(ci => ci.GetParameters().Length);

      return constructors.SingleOrDefaultForAny(ci => ci.GetParameters().Length == maxParameters);
    }

    public string StrategyDescription
    {
      get { return "Choose public constructor with most arguments"; }
    }
  }
}