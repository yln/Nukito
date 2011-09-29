using System;
using System.Linq;
using Moq;

namespace Nukito.Internal
{
  internal class MoqResolver : IResolver
  {
    private readonly ICreator _creator;

    public MoqResolver(ICreator creator)
    {
      _creator = creator;
    }

    public object Get(Type type)
    {
      if (IsInvalidMockType(type))
      {
        throw new NukitoException("The generic version Mock<T> must be used in place of Mock");
      }
      return IsMockType(type)
               ? CreateMock(type)
               : _creator.Create(type);
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

    internal Mock CreateMock(Type type)
    {
      Type serviceType = type.GetGenericArguments().Single();
      var mocked = _creator.Create(serviceType) as IMocked;
      if (mocked == null)
      {
        throw new NukitoException(string.Format("Can not create mock for type {0}", serviceType.FullName));
      }
      return mocked.Mock;
    }
  }
}