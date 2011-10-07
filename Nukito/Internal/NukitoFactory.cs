﻿using Moq;
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

    private static ITestCommand CreateNuktioCommand(IMethodInfo methodInfo, INukitoSettings settings)
    {
      var constructorChooser = new CompositeConstructorChooser(_ConstructorChoosers);
      var mockRepository = new MockRepository(settings.MockBehavior)
                             {CallBase = settings.CallBase, DefaultValue = settings.DefaultValue};
      var mockHandler = new MoqMockHandler(mockRepository);
      var creator = new Creator(constructorChooser, mockHandler);
      var resolver = new MoqResolver(creator);
      var verifier = new Verifier(mockHandler, settings.MockVerification);

      return new NukitoFactCommand(methodInfo, resolver, verifier);
    }

    public static ITestCommand CreateCommand(IMethodInfo methodInfo, INukitoSettings settings)
    {
      return methodInfo.MethodInfo.GetParameters().Length == 0
               ? new FactCommand(methodInfo)
               : CreateNuktioCommand(methodInfo, settings);
    }
  }
}