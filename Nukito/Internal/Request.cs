using System;
using System.Collections.Generic;

namespace Nukito.Internal
{
  public class Request
  {
    public Request (Type type, bool forceMockCreation, MockSettings settings, IDictionary<Type, object> instances)
    {
      Type = type;
      ForceMockCreation = forceMockCreation;
      Settings = settings;
      Instances = instances;
    }

    private Request (Request parent, Type type)
    {
      Parent = parent;
      Type = type;

      Settings = parent.Settings;
      Instances = parent.Instances;
    }

    public Request CreateSubRequest (Type type)
    {
      return new Request (this, type);
    }

    public Request Parent { get; private set; }
    public Type Type { get; private set; }
    public bool ForceMockCreation { get; private set; }
    public MockSettings Settings { get; private set; }
    public IDictionary<Type, object> Instances { get; private set; }
  }
}