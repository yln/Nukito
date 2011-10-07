using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Nukito.Internal;
using Xunit;
using Xunit.Sdk;

namespace Nukito
{
  public class NukitoFactAttribute : FactAttribute, INukitoSettingsBuilder
  {
    public NukitoFactAttribute()
    {
      Settings = new NukitoSettings();
    }

    protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
    {
      yield return NukitoFactory.CreateCommand(method, GetSettings(method));
    }

    private INukitoSettings GetSettings(IMethodInfo method)
    {
      var settingsToMerge = new List<NukitoSettings>();
      IAttributeInfo classSettingsAttribute =
        method.Class.GetCustomAttributes(typeof (NukitoSettingsAttribute)).SingleOrDefault();
      if (classSettingsAttribute != null)
      {
        var classSettings = classSettingsAttribute.GetInstance<NukitoSettingsAttribute>().Settings;
        settingsToMerge.Add(classSettings);
      }
      var methodSettings = Settings;
      settingsToMerge.Add(methodSettings);

      return NukitoSettings.Merge(settingsToMerge);
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