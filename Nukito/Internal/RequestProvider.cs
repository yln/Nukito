using System;
using System.Collections.Generic;

namespace Nukito.Internal
{
  public class RequestProvider : IRequestProvider
  {
    private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();

    public Request GetRequest (Type parameterType, MockSettings settings)
    {
      return new Request (parameterType, false, settings, _instances);
    }
  }
}