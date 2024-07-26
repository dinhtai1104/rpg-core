using System;
using System.Reflection;

namespace Assets.Abstractions.Shared.Pool.Factory
{
    public class NonPublicObjectFactory<T> : IObjectFactory<T>
    {
        public T Create()
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + typeof(T));
            }

            return (T)ctor.Invoke(null);
        }
    }
}
