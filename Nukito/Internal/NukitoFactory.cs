using Ninject.MockingKernel.Moq;
using Xunit.Sdk;

namespace Nukito.Internal
{
  internal class NukitoFactory
  {
    private readonly INukitoSettings _settings;

    public NukitoFactory(INukitoSettings settings)
    {
      _settings = settings;

      NewInstances();
    }

    public ICreator Creator { get; private set; }
    public IResolver Resolver { get; private set; }
    public IVerifier Verifier { get; private set; }

    public NukitoFactory NewInstances()
    {
      var kernel = new MoqMockingKernel();
      kernel.Settings.SetMockBehavior(_settings.MockBehavior);

      Creator = new NinjectCreator(kernel);
      Resolver = new MoqResolver(Creator);
      Verifier = new MoqVerifier(kernel.MockRepository, _settings.MockVerification);

      return this;
    }

    public ITestCommand CreateCommand(IMethodInfo methodInfo)
    {
      return new NukitoFactCommand(methodInfo, Resolver, Verifier);
    }
  }
}