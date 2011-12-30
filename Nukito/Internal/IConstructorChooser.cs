using System;
using System.Reflection;

namespace Nukito.Internal
{
  internal interface IConstructorChooser
  {
    ConstructorInfo GetConstructor(Type type);

    string StrategyDescription { get; }
  }
}