using System;
using System.Buffers;

namespace Assets.Abstractions.Shared.Foundation.collections
{
	partial struct PooledList<T> : IDisposable
	{
		public PooledList(T[] items) : this(items.AsSpan(), ArrayPool<T>.Shared) { }

		public PooledList(T[] items, ArrayPool<T> pool) : this(items.AsSpan(), pool) { }

		public PooledList(in ReadOnlySpan<T> span) : this(span, ArrayPool<T>.Shared) { }

		public PooledList(in ReadOnlySpan<T> span, ArrayPool<T> pool)
		{
			_pool = pool ?? ArrayPool<T>.Shared;

			var count = span.Length;

			if (count == 0)
			{
				_items = s_emptyArray;
				_size = 0;
			}
			else
			{
				_items = _pool.Rent(count);
				span.CopyTo(_items);
				_size = count;
			}

			_version = 0;
		}

		public void ConvertAll<TOut>(PooledList<TOut> output, Converter<T, TOut> converter)
		{
			if (converter == null)
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.converter);

			for (var i = 0; i < _size; i++)
			{
				output.Add(converter(_items[i]));
			}
		}

		public void FindAll(PooledList<T> output, Predicate<T> match)
		{
			if (match == null)
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

			for (var i = 0; i < _size; i++)
			{
				if (match(_items[i]))
				{
					output.Add(_items[i]);
				}
			}
		}

		public bool TryFind(Predicate<T> match, out T result)
		{
			if (match == null)
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

			for (var i = 0; i < _size; i++)
			{
				if (match(_items[i]))
				{
					result = _items[i];
					return true;
				}
			}

			result = default;
			return false;
		}

		public bool TryFindLast(Predicate<T> match, out T result)
		{
			if (match is null)
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.match);

			for (var i = _size - 1; i >= 0; i--)
			{
				if (match(_items[i]))
				{
					result = _items[i];
					return true;
				}
			}

			result = default;
			return false;
		}

		public void Dispose()
		{
			ReturnArray(s_emptyArray);
			_size = 0;
			_version++;
		}
	}
}
