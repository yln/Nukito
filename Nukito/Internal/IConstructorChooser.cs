using System;
using System.Reflection;

namespace Nukito.Internal
{
  [VisibleForTesting]
  public interface IConstructorChooser
  {
    ConstructorInfo GetConstructor(Type type);

    string StrategyDescription { get; }
  }
}