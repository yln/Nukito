﻿using Ninject.MockingKernel.Moq;

namespace Nukito.Internal
{
  internal class NukitoFactory
  {
    private readonly INukitoSettings _nukitoSettings;

    public NukitoFactory()
      : this(new NukitoSettingsAttribute())
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