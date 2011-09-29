using System.Collections.Generic;
using System.Linq;
using Nukito.Internal;
using Xunit;
using Xunit.Sdk;

namespace Nukito
{
  public class NukitoFactAttribute : FactAttribute
  {
    // TODO: Maybe make this static to allow for class-wide configuration settings
    // In combination with a "Reset" method
    //private readonly IResolver _resolver = new MoqResolver();

    protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
    {
      IAttributeInfo attributeInfo = method.GetCustomAttributes(typeof (NukitoSettingsAttribute)).SingleOrDefault();
      INukitoSettings nukitoSettings = attributeInfo != null
                                         ? attributeInfo.GetInstance<NukitoSettingsAttribute>()
                                         : new NukitoSettingsAttribute();
      IResolver resolver = new NukitoFactory(nukitoSettings).NewResolver();

      yield return new NukitoFactCommand(method, resolver);
    }
  }
}