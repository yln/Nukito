using System;

namespace Nukito.Internal
{
  internal interface ICreator
  {
    object Create(Type type);
  }
}