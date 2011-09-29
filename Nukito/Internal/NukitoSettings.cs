using System.Collections.Generic;
using Moq;

namespace Nukito.Internal
{
  internal class NukitoSettings : INukitoSettings
  {
    public const string MockBehaviorKey = "MockBehavior";

    private readonly IDictionary<string, object> _settings;

    public NukitoSettings(IDictionary<string, object> settings)
    {
      _settings = settings;
    }

    private T Get<T>(string key, T defaultValue)
    {
      object value;
      if (_settings.TryGetValue(key, out value))
      {
        return (T) value;
      }
      return defaultValue;
    }

    public MockBehavior MockBehavior
    {
      get { return Get(MockBehaviorKey, MockBehavior.Default); }
    }
  }
}