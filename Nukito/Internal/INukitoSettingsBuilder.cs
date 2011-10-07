using Moq;

namespace Nukito.Internal
{
  internal interface INukitoSettingsBuilder
  {
    MockBehavior MockBehavior { set; }
    bool CallBase { set; }
    DefaultValue DefaultValue { set; }

    MockVerification MockVerification { set; }
  }
}