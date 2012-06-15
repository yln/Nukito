using System;

namespace Nukito.Internal
{
  public interface IMockHandler
  {
    object CreateMock(Type type);

    void VerifyAll();
    void VerifyMarked();
  }
}