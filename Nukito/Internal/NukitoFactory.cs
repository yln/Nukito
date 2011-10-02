using Moq;
using Nukito.Internal.ConstructorChooser;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal static class NukitoFactory
  {
    private static readonly IConstructorChooser[] _ConstructorChoosers =
      new IConstructorChooser[]
        {
          new SinglePublicConstructorChooser(),
          new SinglePublicWithInjectAttributeConstructorChooser(),
          new MaxArgumentsPublicConstructorChooser(),
          new SingleInternalConstructorChooser(),
          new SingleInternalWithInjectAttributeConstructorChooser(),
          new MaxArgumentsInternalConstructorChooser(),
        };

    public static ITestCommand CreateCommand(IMethodInfo methodInfo, INukitoSettings settings)
    {
      var constructorChooser = new CompositeConstructorChooser(_ConstructorChoosers);
      var mockRepository = new MockRepository(settings.MockBehavior);
      var mockHanlder = new MoqMockHandler(mockRepository);
      var creator = new Creator(constructorChooser, mockHanlder);
      var resolver = new MoqResolver(creator);
      var mockHandler = new MoqMockHandler(mockRepository);
      var verifier = new Verifier(mockHandler, settings.MockVerification);

      return new NukitoFactCommand(methodInfo, resolver, verifier);
    }
  }
}