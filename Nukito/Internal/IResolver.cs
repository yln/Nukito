using System;

namespace Nukito.Internal
{
  public interface IResolver
  {
    object Get(Type type);
  }
}