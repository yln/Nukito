using Moq;

namespace Nukito.Internal
{
  internal interface INukitoSettingsBuilder
  {
    MockBehavior MockBehavior { set; }
    MockVerification MockVerification { set; }
  }
}