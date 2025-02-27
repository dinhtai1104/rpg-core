using System;
using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Core.DI
{
    internal static class DictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!source.TryGetValue(key, out var value))
            {
                value = valueFactory.Invoke(key);
                source.Add(key, value);
            }

            return value;
        }
    }
}
