using System;
using Ninject;

namespace Nukito.Internal
{
  internal class NinjectCreator : ICreator
  {
    private readonly IKernel _kernel;

    public NinjectCreator(IKernel kernel)
    {
      _kernel = kernel;
    }

    public object Create(Type type)
    {
      return _kernel.Get(type);
    }
  }
}