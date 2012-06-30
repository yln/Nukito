using System;
using System.Collections.Generic;

namespace Nukito.Internal
{
  public class RequestProvider : IRequestProvider
  {
    private readonly Dictionary<string, Dictionary<Type, object>> _instanceContexts = new Dictionary<string, Dictionary<Type, object>> ();

    public Request GetRequest (string contextName, Type parameterType, MockSettings settings)
    {
      var instances = GetOrCreateContext (contextName);
      return new Request (parameterType, false, settings, instances);
    }

    private IDictionary<Type, object> GetOrCreateContext (string contextName)
    {
      Dictionary<Type, object> instances;
      if (!_instanceContexts.TryGetValue (contextName, out instances))
      {
        instances = new Dictionary<Type, object>();
        _instanceContexts.Add (contextName, instances);
      }

      return instances;
    }
  }
}