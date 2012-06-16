using System;

namespace Nukito.Internal
{
  public interface IMockRepository
  {
    object WrappedRepository { get; }

    bool IsMockRepositoryType (Type type);

    object CreateMock (Type serviceType, MockSettings settings);

    void VerifyMocks (MockVerification mockVerification);
  }
}