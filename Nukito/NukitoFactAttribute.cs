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
      Settings = new Dictionary<string, object>();
    }

    protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
    {
      INukitoSettings settings = GetSettings(method);
      IResolver resolver = new NukitoFactory(settings).NewResolver();

      yield return new NukitoFactCommand(method, resolver);
    }

    private INukitoSettings GetSettings(IMethodInfo method)
    {
      var mergedSettings = new Dictionary<string, object>();
      IAttributeInfo classSettingsAttribute =
        method.Class.GetCustomAttributes(typeof (NukitoSettingsAttribute)).SingleOrDefault();
      if (classSettingsAttribute != null)
      {
        var classSettings = classSettingsAttribute.GetInstance<NukitoSettingsAttribute>().Settings;
        mergedSettings.AddOrReplaceAll(classSettings);
      }
      var methodSettings = Settings;
      mergedSettings.AddOrReplaceAll(methodSettings);

      return new NukitoSettings(mergedSettings);
    }

    public IDictionary<string, object> Settings { get; private set; }

    public MockBehavior MockBehavior
    {
      get { throw new NotImplementedException(); }
      set { Settings[NukitoSettings.MockBehaviorKey] = value; }
    }

    public MockVerification MockVerification
    {
      get { throw new NotImplementedException(); }
      set { Settings[NukitoSettings.MockVerificationKey] = value; }
    }
  }
}