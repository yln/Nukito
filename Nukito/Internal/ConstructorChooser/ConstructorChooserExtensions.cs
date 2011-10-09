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
        return !e.MoveNext() ? result : default(TSource);
      }
    }

    public static TSource SingleOrDefaultForAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
      using (IEnumerator<TSource> e = source.GetEnumerator())
      {
        TSource result = default(TSource);
        while (e.MoveNext())
        {
          if (predicate(e.Current))
          {
            result = e.Current;
            break;
          }
        }
        while (e.MoveNext())
        {
          if (predicate(e.Current))
          {
            return default(TSource);
          }
        }
        return result;
      }
    }

    public static IEnumerable<ConstructorInfo> GetInternalConstructors(this Type type)
    {
      return type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
        .Where(ci => ci.IsAssembly);
    }
  }
}