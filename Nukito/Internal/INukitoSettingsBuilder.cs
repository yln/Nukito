using System.Collections.Generic;
using Moq;

namespace Nukito.Internal
{
  internal interface INukitoSettingsBuilder
  {
    IDictionary<string, object> Settings { get; }

    MockBehavior MockBehavior { set; }
    MockVerification MockVerification { set; }
  }
}