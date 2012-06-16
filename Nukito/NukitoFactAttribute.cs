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
      var constructors = method.Class.Type.GetConstructors();
      if (constructors.Length != 1)
        throw new NukitoException("Test class must have a single public constructor");

      var ctor = constructors.Single ();
      var settings = MockSettingsAttribute.GetSettings (method.MethodInfo);
      var ctorSettings = MockSettingsAttribute.GetSettings (ctor);
      var command = NukitoFactory.CreateCommand (method, ctor, settings, ctorSettings);

      return new[] { command };
    }
  }
}