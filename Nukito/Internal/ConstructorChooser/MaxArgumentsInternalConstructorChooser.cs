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
      if (constructors.Length == 0)
      {
        return null;
      }
      int maxParameters = constructors.Max(ci => ci.GetParameters().Length);

      return constructors.SingleOrDefaultForAny(ci => ci.GetParameters().Length == maxParameters);
    }

    public string StrategyDescription
    {
      get { return "Internal constructor with most arguments"; }
    }
  }
}