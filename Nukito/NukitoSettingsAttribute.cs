using System;
using Moq;
using Nukito.Internal;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class NukitoSettingsAttribute : Attribute, INukitoSettingsBuilder
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

    public MockVerification MockVerification
    {
      get { throw new NotImplementedException(); }
      set { Settings.MockVerification = value; }
    }
  }
}