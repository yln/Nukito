using System;
using System.Reflection;
using Moq;

namespace Nukito.Internal
{
  internal class MoqMockHandler : IMockHandler
  {
    private static readonly MethodInfo _CreateMethod = typeof (MockRepository).GetMethod("Create", new Type[0]);

    private readonly MockRepository _mockRepository;

    public MoqMockHandler(MockRepository mockRepository)
    {
      _mockRepository = mockRepository;
    }

    public object CreateMock(Type type)
    {
      MethodInfo methodInfo = _CreateMethod.MakeGenericMethod(type);
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