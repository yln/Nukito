using System;
using Moq;

namespace Nukito.Internal
{
  internal class MoqMockHandler : IMockHandler
  {
    private readonly MockRepository _mockRepository;

    public MoqMockHandler(MockRepository mockRepository)
    {
      _mockRepository = mockRepository;
    }

    public object CreateMock(Type type)
    {
      throw new NotImplementedException();
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