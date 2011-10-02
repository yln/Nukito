﻿using System;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal class SingleInternalConstructorChooser : IConstructorChooser
  {
    public ConstructorInfo GetConstructor(Type type)
    {
      return type.GetInternalConstructors().SingleOrDefaultForAny();
    }

    public string StrategyDescription
    {
      get { return "Single internal constructor"; }
    }
  }
}