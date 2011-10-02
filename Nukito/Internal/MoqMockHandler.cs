using System;
using System.Reflection;
using Moq;

namespace Nukito.Internal
{
  internal class MoqMockHandler : IMockHandler
  {
    private static readonly object _Locker = new object();
    private static MethodInfo _createMethod;

    private static MethodInfo CreateMethod
    {
      get
      {
        lock (_Locker)
        {
          return _createMethod ?? (_createMethod = typeof (MockRepository).GetMethod("Create", new Type[0]));
        }
      }
    }

    private readonly MockRepository _mockRepository;

    public MoqMockHandler(MockRepository mockRepository)
    {
      _mockRepository = mockRepository;
    }

    public object CreateMock(Type type)
    {
      MethodInfo methodInfo = CreateMethod.MakeGenericMethod(type);
      var mock = (Mock) methodInfo.Invoke(_mockRepository, new object[0]);
      return mock.Object;
    }

    public void VerifyAll()
    {
      _mockRepository.VerifyAll();
    }

    public void VerifyMarked()
    {
      _mockRepository.Verify();
    }
  }
}