using System;
using Moq;

namespace Nukito.Internal
{
  [VisibleForTesting]
  public interface IMockHandler
  {
    Mock CreateMock(Type type);

    void VerifyAll();
    void VerifyMarked();
  }
}