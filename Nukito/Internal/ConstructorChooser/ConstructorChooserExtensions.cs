using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nukito.Internal.ConstructorChooser
{
  internal static class ConstructorChooserExtensions
  {
    public static TSource SingleOrDefaultForAny<TSource>(this IEnumerable<TSource> source)
    {
      using (IEnumerator<TSource> e = source.GetEnumerator())
      {
        if (!e.MoveNext()) return default(TSource);
        TSource result = e.Current;
        if (!e.MoveNext()) return result;
      }
      return default(TSource);
    }

    public static TSource SingleOrDefaultForAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      TSource result = default(TSource);
      int count = 0;
      foreach (TSource element in source)
      {
        if (!predicate(element)) continue;

        if (count != 0)
        {
          result = default(TSource);
          break;
        }
        result = element;
        count++;
      }
      return result;
    }

    public static IEnumerable<ConstructorInfo> GetInternalConstructors(this Type type)
    {
      return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(ci => ci.IsAssembly);
    }
  }
}