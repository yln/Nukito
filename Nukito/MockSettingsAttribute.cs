using System;
using System.Linq;
using System.Reflection;
using Moq;
using Nukito.Internal;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
  public class MockSettingsAttribute : Attribute
  {
    public static MockSettings GetSettings(MethodBase methodBase)
    {
      var classSettings = GetSettingsOrDefaultFor(methodBase.ReflectedType);
      var methodSettings = GetSettingsOrDefaultFor(methodBase);

      return MockSettings.Merge(classSettings, methodSettings);
    }

    private static MockSettings GetSettingsOrDefaultFor(MemberInfo member)
    {
      var attribute = (MockSettingsAttribute) member.GetCustomAttributes (typeof (MockSettingsAttribute), true).FirstOrDefault ();
      return attribute != null ? attribute.Settings : new MockSettings();
    }

    public MockSettingsAttribute()
    {
      Settings = new MockSettings();
    }

    internal MockSettings Settings { get; private set; }

    public MockBehavior Behavior
    {
      get { throw new NotImplementedException(); }
      set { Settings.Behavior = value; }
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

    public MockVerification Verification
    {
      get { throw new NotImplementedException(); }
      set { Settings.Verification = value; }
    }
  }
}