using System;

namespace Nukito.Internal
{
  [VisibleForTesting]
  public interface IMockHandler
  {
    object CreateMock(Type type);

    void VerifyAll();
    void VerifyMarked();
  }
}