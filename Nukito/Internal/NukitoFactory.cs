using Ninject.MockingKernel.Moq;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal static class NukitoFactory
  {
    public static ITestCommand CreateCommand(IMethodInfo methodInfo, INukitoSettings settings)
    {
      var kernel = new MoqMockingKernel();
      kernel.Settings.SetMockBehavior(settings.MockBehavior);

      var creator = new NinjectCreator(kernel);
      var resolver = new MoqResolver(creator);
      var mockHandler = new MoqMockHandler(kernel.MockRepository);
      var verifier = new Verifier(mockHandler, settings.MockVerification);

      return new NukitoFactCommand(methodInfo, resolver, verifier);
    }
  }
}