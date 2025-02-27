using System;

namespace Assets.Abstractions.Shared.Foundation
{
    public abstract class Singleton<T> where T : class, new()
    {
        public static T Instance => instance.Value;

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        private Singleton() { }
    }
}