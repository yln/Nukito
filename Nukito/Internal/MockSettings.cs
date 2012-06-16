using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Nukito.Internal
{
  public class MockSettings
  {
    private const string BehaviorKey = "Behavior";
    private const string CallBaseKey = "CallBase";
    private const string DefaultValueKey = "DefaultValue";
    private const string VerificationKey = "Verification";

    private readonly Dictionary<string, object> _settings = new Dictionary<string, object>();

    public static MockSettings Merge(MockSettings classSettings, MockSettings methodSettings)
    {
      var merged = new MockSettings();

      // Method settings overwrite class settings
      foreach (var entry in classSettings._settings.Concat (methodSettings._settings))
        merged._settings[entry.Key] = entry.Value;
      
      return merged;
    }

    private T Get<T>(string key, T defaultValue)
    {
      object value;
      if (_settings.TryGetValue(key, out value))
        return (T) value;

      return defaultValue;
    }

    public MockBehavior Behavior
    {
      get { return Get(BehaviorKey, MockBehavior.Default); }
      internal set { _settings[BehaviorKey] = value; }
    }

    public bool CallBase
    {
      get { return Get(CallBaseKey, false); }
      internal set { _settings[CallBaseKey] = value; }
    }

    public DefaultValue DefaultValue
    {
      get { return Get(DefaultValueKey, DefaultValue.Mock); }
      internal set { _settings[DefaultValueKey] = value; }
    }

    public MockVerification Verification
    {
      get { return Get(VerificationKey, MockVerification.All); }
      internal set { _settings[VerificationKey] = value; }
    }
  }
}