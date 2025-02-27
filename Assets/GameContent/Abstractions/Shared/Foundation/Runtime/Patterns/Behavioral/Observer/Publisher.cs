using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> Publisher. </summary>
    public abstract class Publisher
    {
        private readonly List<IObserver> observers = new List<IObserver>();

        /// <summary> Add an observer. </summary>
        /// <param name="observer">Observer</param>
        public void AddObserver(IObserver observer) => observers.Add(observer);

        /// <summary> Add observers. </summary>
        /// <param name="observer">Array of abservers</param>
        public void AddObservers(IEnumerable<IObserver> observers) => this.observers.AddRange(observers);

        /// <summary> Remove an observer. </summary>
        /// <param name="observer">Observer</param>
        public void RemoveObserver(IObserver observer) => observers.Remove(observer);

        /// <summary> Remove observers. </summary>
        /// <param name="observer">Observers</param>
        public void RemoveObservers(IEnumerable<IObserver> observers) => this.observers.RemoveRange(observers);

        /// <summary> Notifies all its observers. </summary>
        protected void Notify()
        {
            for (int i = 0; i < observers.Count; ++i)
                observers[i].OnNotify();
        }
    }

    /// <summary> Publisher. </summary>
    public abstract class Publisher<T>
    {
        private readonly List<IObserver<T>> observers = new List<IObserver<T>>();

        /// <summary> Add an observer. </summary>
        /// <param name="observer">Observer</param>
        public void AddObserver(IObserver<T> observer) => observers.Add(observer);

        /// <summary> Add observers. </summary>
        /// <param name="observer">Array of abservers</param>
        public void AddObservers(IEnumerable<IObserver<T>> observers) => this.observers.AddRange(observers);

        /// <summary> Remove an observer. </summary>
        /// <param name="observer">Observer</param>
        public void RemoveObserver(IObserver<T> observer) => observers.Remove(observer);

        /// <summary> Remove observers. </summary>
        /// <param name="observer">Observers</param>
        public void RemoveObservers(IEnumerable<IObserver<T>> observers) => this.observers.RemoveRange(observers);

        /// <summary> Notifies all its observers. </summary>
        protected void Notify(T value)
        {
            for (int i = 0; i < observers.Count; ++i)
                observers[i].OnNotify(value);
        }
    }

    /// <summary> Publisher. </summary>
    public abstract class Publisher<T0, T1>
    {
        private readonly List<IObserver<T0, T1>> observers = new List<IObserver<T0, T1>>();

        /// <summary> Add an observer. </summary>
        /// <param name="observer">Observer</param>
        public void AddObserver(IObserver<T0, T1> observer) => observers.Add(observer);

        /// <summary> Add observers. </summary>
        /// <param name="observer">Array of abservers</param>
        public void AddObservers(IEnumerable<IObserver<T0, T1>> observers) => this.observers.AddRange(observers);

        /// <summary> Remove an observer. </summary>
        /// <param name="observer">Observer</param>
        public void RemoveObserver(IObserver<T0, T1> observer) => observers.Remove(observer);

        /// <summary> Remove observers. </summary>
        /// <param name="observer">Observers</param>
        public void RemoveObservers(IEnumerable<IObserver<T0, T1>> observers) => this.observers.RemoveRange(observers);

        /// <summary> Notifies all its observers. </summary>
        protected void Notify(T0 value0, T1 value1)
        {
            for (int i = 0; i < observers.Count; ++i)
                observers[i].OnNotify(value0, value1);
        }
    }

    /// <summary> Publisher. </summary>
    public abstract class Publisher<T0, T1, T2>
    {
        private readonly List<IObserver<T0, T1, T2>> observers = new List<IObserver<T0, T1, T2>>();

        /// <summary> Add an observer. </summary>
        /// <param name="observer">Observer</param>
        public void AddObserver(IObserver<T0, T1, T2> observer) => observers.Add(observer);

        /// <summary> Add observers. </summary>
        /// <param name="observer">Array of abservers</param>
        public void AddObservers(IEnumerable<IObserver<T0, T1, T2>> observers) => this.observers.AddRange(observers);

        /// <summary> Remove an observer. </summary>
        /// <param name="observer">Observer</param>
        public void RemoveObserver(IObserver<T0, T1, T2> observer) => observers.Remove(observer);

        /// <summary> Remove observers. </summary>
        /// <param name="observer">Observers</param>
        public void RemoveObservers(IEnumerable<IObserver<T0, T1, T2>> observers) => this.observers.RemoveRange(observers);

        /// <summary> Notifies all its observers. </summary>
        protected void Notify(T0 value0, T1 value1, T2 value2)
        {
            for (int i = 0; i < observers.Count; ++i)
                observers[i].OnNotify(value0, value1, value2);
        }
    }
}