using System.Collections.Generic;
using System.Linq;
using Nukito.Internal;
using Xunit;
using Xunit.Sdk;

namespace Nukito
{
  public class NukitoFactAttribute : FactAttribute
  {
    protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
    {
      yield return NukitoFactory.CreateCommand(method, GetSettings(method));
    }

    private NukitoSettings GetSettings(IMethodInfo method)
    {
      IAttributeInfo classSettingsAttribute = method.Class.GetCustomAttributes (typeof (NukitoSettingsAttribute)).SingleOrDefault ();
      IAttributeInfo methodSettingsAttribute = method.GetCustomAttributes (typeof (NukitoSettingsAttribute)).SingleOrDefault ();

      var settingsToMerge = new List<NukitoSettings> ();
      AddSettingsFromAttributeToList(settingsToMerge, classSettingsAttribute);
      AddSettingsFromAttributeToList(settingsToMerge, methodSettingsAttribute);

      return NukitoSettings.Merge(settingsToMerge);
    }

    private void AddSettingsFromAttributeToList(List<NukitoSettings> settingsToMerge, IAttributeInfo settingsAttributeOrNull)
    {
      if (settingsAttributeOrNull != null)
        settingsToMerge.Add(settingsAttributeOrNull.GetInstance<NukitoSettingsAttribute>().Settings);
    }
  }
}