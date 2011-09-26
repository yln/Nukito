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
        throw new NukitoException("The generic version Mock<T> must be used in place of Mock");
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
      var mocked = _mockingKernel.Get(serviceType) as IMocked;
      if (mocked == null)
      {
        string msg = string.Format("Can not create mock for type {0}", serviceType.FullName);
        throw new NukitoException(msg);
      }
      return mocked.Mock;
    }
  }
}