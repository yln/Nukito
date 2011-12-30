using System;

namespace Nukito.Internal
{
  internal interface IMockHandler
  {
    object CreateMock(Type type);

    void VerifyAll();
    void VerifyMarked();
  }
}