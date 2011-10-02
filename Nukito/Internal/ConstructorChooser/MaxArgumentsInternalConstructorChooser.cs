using System;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class MaxArgumentsInternalConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      ConstructorInfo[] constructors = type.GetInternalConstructors().ToArray();
      int maxParameters = constructors.Max(ci => ci.GetParameters().Length);

      return constructors.SingleOrDefaultForAny(ci => ci.GetParameters().Length == maxParameters);
    }

    public string StrategyDescription
    {
      get { return "Choose internal constructor with most arguments"; }
    }
  }
}