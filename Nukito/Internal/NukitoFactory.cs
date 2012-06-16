using System.Reflection;
using Nukito.Internal.ConstructorChooser;
using Nukito.Internal.Moq;
using Xunit.Sdk;

namespace Nukito.Internal
{
  public static class NukitoFactory
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

    public static ITestCommand CreateCommand (IMethodInfo methodInfo, ConstructorInfo constructor, MockSettings settings, MockSettings constructorSettings)
    {
      var reflectionHelper = new ReflectionHelper();
      var mockRepository = new MoqMockRepository (reflectionHelper);
      var constructorChooser = new CompositeConstructorChooser (s_constructorChoosers);
      var resolver = new Resolver (mockRepository, constructorChooser, reflectionHelper);
      var moqResolver = new MoqResolver (resolver);

      return new NukitoFactCommand (methodInfo, constructor, reflectionHelper, moqResolver, mockRepository, settings, constructorSettings);
    }
  }
}