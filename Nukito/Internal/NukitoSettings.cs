using System.Collections.Generic;
using Moq;

namespace Nukito.Internal
{
  internal class NukitoSettings : INukitoSettings
  {
    private const string MockBehaviorKey = "MockBehavior";
    private const string MockVerificationKey = "MockVerification";

    private readonly IDictionary<string, object> _settings;

    public NukitoSettings()
    {
      _settings = new Dictionary<string, object>();
    }

    public static NukitoSettings Merge(IEnumerable<NukitoSettings> settingsToMerge)
    {
      var merged = new NukitoSettings();

      foreach (NukitoSettings additional in settingsToMerge)
      {
        foreach (KeyValuePair<string, object> additionalEntry in additional._settings)
        {
          merged._settings[additionalEntry.Key] = additionalEntry.Value;
        }
      }

      return merged;
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
      set { _settings[MockBehaviorKey] = value; }
    }

    public MockVerification MockVerification
    {
      get { return Get(MockVerificationKey, MockVerification.All); }
      set { _settings[MockVerificationKey] = value; }
    }
  }
}