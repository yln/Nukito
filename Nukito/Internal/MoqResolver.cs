using System;
using System.Linq;
using Moq;
using Ninject;
using Ninject.MockingKernel.Moq;

namespace Nukito.Internal
{
  internal class MoqResolver : IResolver
  {
    private readonly MoqMockingKernel _mockingKernel = new MoqMockingKernel();

    public object Get(Type type)
    {
      if (IsInvalidMockType(type))
      {
        throw new NukitoException("Please use the generic version Moq.Mock<T> instead of Moq.Mock");
      }
      return IsMockType(type)
               ? GetMock(type)
               : _mockingKernel.Get(type);
    }

    internal bool IsInvalidMockType(Type type)
    {
      return type == typeof (Mock);
    }

    internal bool IsMockType(Type type)
    {
      return type.IsGenericType
             && type.GetGenericTypeDefinition() == typeof (Mock<>);
    }

    internal Mock GetMock(Type type)
    {
      Type serviceType = type.GetGenericArguments().Single();
      object servce = _mockingKernel.Get(serviceType);
      return ((IMocked) servce).Mock;
    }
  }
}