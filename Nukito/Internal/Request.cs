using System;
using System.Collections.Generic;

namespace Nukito.Internal
{
  public class Request
  {
    public Request (Type type, bool forceMockCreation, MockSettings settings)
    {
      Type = type;
      ForceMockCreation = forceMockCreation;
      Settings = settings;

      Instances = new Dictionary<Type, object>();
    }

    //public Request (Request parent, Type type, bool forceMockCreation)
    //{
    //  Parent = parent;
    //  Type = type;
    //  ForceMockCreation = forceMockCreation;

    //  Settings = parent.Settings;
    //  Instances = parent.Instances;
    //}

    public Request Parent { get; private set; }
    public Type Type { get; private set; }
    public bool ForceMockCreation { get; private set; }
    public MockSettings Settings { get; private set; }
    public IDictionary<Type, object> Instances { get; private set; }
  }
}