using System;
using Moq;
using Nukito.Internal;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
  public class NukitoSettingsAttribute : Attribute
  {
    public NukitoSettingsAttribute()
    {
      Settings = new NukitoSettings();
    }

    internal NukitoSettings Settings { get; private set; }

    public MockBehavior MockBehavior
    {
      get { throw new NotImplementedException(); }
      set { Settings.MockBehavior = value; }
    }

    public bool CallBase
    {
      get { throw new NotImplementedException(); }
      set { Settings.CallBase = value; }
    }

    public DefaultValue DefaultValue
    {
      get { throw new NotImplementedException(); }
      set { Settings.DefaultValue = value; }
    }

    public MockVerification MockVerification
    {
      get { throw new NotImplementedException(); }
      set { Settings.MockVerification = value; }
    }
  }
}