using System;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class SinglePublicConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      return type.GetConstructors().SingleOrDefaultForAny();
    }

    public string StrategyDescription
    {
      get { return "Single public constructor"; }
    }
  }
}