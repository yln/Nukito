using System;
using Moq;
using Nukito.Internal;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
  public class NukitoSettingsAttribute : Attribute, INukitoSettings
  {
    public NukitoSettingsAttribute()
    {
      MockBehavior = MockBehavior.Default;
    }

    public MockBehavior MockBehavior { get; set; }
  }
}