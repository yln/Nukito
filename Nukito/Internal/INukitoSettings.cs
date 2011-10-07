using Moq;

namespace Nukito.Internal
{
  internal interface INukitoSettings
  {
    MockBehavior MockBehavior { get; }
    bool CallBase { get; }
    DefaultValue DefaultValue { get; }

    MockVerification MockVerification { get; }
  }
}