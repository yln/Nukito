using System;

namespace Nukito.Internal
{
  public interface IRequestProvider
  {
    Request GetRequest (string contextName, Type parameterType, MockSettings settings);
  }
}