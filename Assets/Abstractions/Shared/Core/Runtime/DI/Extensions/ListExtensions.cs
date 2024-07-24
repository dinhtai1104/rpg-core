using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Abstractions.Shared.Core.DI
{
	internal static class ListExtensions
	{
		internal static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
		{
			return source.OrderByDescending(selector).First();
		}
	}
}
