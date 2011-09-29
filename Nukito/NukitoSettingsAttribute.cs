using System;
using System.Collections.Generic;
using Moq;
using Nukito.Internal;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NukitoSettingsAttribute : Attribute, INukitoSettingsBuilder
  {
    public NukitoSettingsAttribute()
    {
      Settings = new Dictionary<string, object>();
    }

    public IDictionary<string, object> Settings { get; private set; }

    public MockBehavior MockBehavior
    {
      get { throw new NotImplementedException(); }
      set { Settings[NukitoSettings.MockBehaviorKey] = value; }
    }
  }
}