using System.Collections.Generic;
using Ninject.MockingKernel.Moq;

namespace Nukito.Internal
{
  internal class NukitoFactory
  {
    private readonly INukitoSettings _nukitoSettings;

    // Just for testing purposes
    public NukitoFactory()
      : this(new NukitoSettings(new Dictionary<string, object>()))
    {
    }

    public NukitoFactory(INukitoSettings nukitoSettings)
    {
      _nukitoSettings = nukitoSettings;
    }

    public ICreator NewCreator()
    {
      var kernel = new MoqMockingKernel();
      kernel.Settings.SetMockBehavior(_nukitoSettings.MockBehavior);

      return new NinjectCreator(kernel);
    }

    public IResolver NewResolver()
    {
      return new MoqResolver(NewCreator());
    }
  }
}