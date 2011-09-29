using System.Collections.Generic;

namespace Nukito.Internal
{
  internal static class MyExtensions
  {
    public static void AddOrReplaceAll<TKey, TValue>(this IDictionary<TKey, TValue> source,
                                                     IEnumerable<KeyValuePair<TKey, TValue>> additionalElements)
    {
      foreach (KeyValuePair<TKey, TValue> element in additionalElements)
      {
        source[element.Key] = element.Value;
      }
    }
  }
}