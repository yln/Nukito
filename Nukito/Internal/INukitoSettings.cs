using Moq;

namespace Nukito.Internal
{
  internal interface INukitoSettings
  {
    MockBehavior MockBehavior { get; }
    MockVerification MockVerification { get; }
  }
}