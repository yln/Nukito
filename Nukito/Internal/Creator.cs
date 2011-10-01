using System;
using System.Collections.Generic;

namespace Nukito.Internal
{
  [VisibleForTesting]
  public class Creator : ICreator
  {
    private readonly IDictionary<Type, object> _instances = new Dictionary<Type, object>();

    public object Create(Type type)
    {
      object obj;
      if (!_instances.TryGetValue(type, out obj))
      {
        obj = CreateNew(type);
        _instances.Add(type, obj);
      }

      return obj;
    }

    private object CreateNew(Type type)
    {
      throw new NotImplementedException();
    }
  }
}