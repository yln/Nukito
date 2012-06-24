using System;

namespace Nukito.Internal
{
  public interface IRequestProvider
  {
    Request GetRequest (Type parameterType, MockSettings settings);
  }
}