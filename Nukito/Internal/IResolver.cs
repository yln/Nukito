using System;

namespace Nukito.Internal
{
  internal interface IResolver
  {
    object Get(Type type);
  }
}