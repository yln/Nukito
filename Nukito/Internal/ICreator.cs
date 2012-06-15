using System;

namespace Nukito.Internal
{
  public interface ICreator
  {
    object Create(Type type);
  }
}