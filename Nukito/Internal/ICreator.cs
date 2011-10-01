using System;

namespace Nukito.Internal
{
  [VisibleForTesting]
  public interface ICreator
  {
    object Create(Type type);
  }
}