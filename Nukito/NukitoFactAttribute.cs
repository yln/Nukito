using System.Collections.Generic;
using Nukito.Internal;
using Xunit;
using Xunit.Sdk;

namespace Nukito
{
  public class NukitoFactAttribute : FactAttribute
  {
    // TODO: Maybe make this static to allow for class-wide configuration settings
    // In combination with a "Reset" method
    private readonly IResolver _resolver = new MoqResolver();

    protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
    {
      yield return new NukitoFactCommand(method, _resolver);
    }
  }
}