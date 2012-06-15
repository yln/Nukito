using Moq;
using Nukito.Internal.ConstructorChooser;
using Nukito.Internal.Moq;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal static class NukitoFactory
  {
    private static readonly IConstructorChooser[] s_constructorChoosers =
      new IConstructorChooser[]
        {
          new SingleCtorWithInjectAttributeConstructorChooser(),
          new SinglePublicConstructorChooser(),
          new MaxArgumentsPublicConstructorChooser(),
          new SingleInternalConstructorChooser(),
          new MaxArgumentsInternalConstructorChooser()
        };

    public static ITestCommand CreateCommand(IMethodInfo methodInfo, NukitoSettings settings)
    {
      var constructorChooser = new CompositeConstructorChooser(s_constructorChoosers);
      var mockRepository = new MockRepository(settings.MockBehavior) {CallBase = settings.CallBase, DefaultValue = settings.DefaultValue};
      var mockHandler = new MoqMockHandler(mockRepository);
      var creator = new Creator(constructorChooser, mockHandler);
      var resolver = new MoqResolver(creator, mockRepository);
      var verifier = new Verifier(mockHandler, settings.MockVerification);

      return new NukitoFactCommand(methodInfo, resolver, verifier);
    }
  }
}