using System;
using System.Linq;
using System.Reflection;
using Moq;

namespace Nukito
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
  public class MockSettingsAttribute : Attribute
  {
    public static Internal.MockSettings GetSettings(MethodBase methodBase)
    {
      var classSettings = GetSettingsOrDefaultFor(methodBase.ReflectedType);
      var methodSettings = GetSettingsOrDefaultFor(methodBase);

      return Internal.MockSettings.Merge(classSettings, methodSettings);
    }

    private static Internal.MockSettings GetSettingsOrDefaultFor(MemberInfo member)
    {
      var attribute = (MockSettingsAttribute) member.GetCustomAttributes (typeof (MockSettingsAttribute), true).FirstOrDefault ();
      return attribute != null ? attribute.Settings : new Internal.MockSettings();
    }

    public MockSettingsAttribute()
    {
      Settings = new Internal.MockSettings();
    }

    internal Internal.MockSettings Settings { get; private set; }

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