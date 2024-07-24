namespace Assets.Abstractions.Shared.Foundation
{
    /// <summary> It allows alter dynamically behaviors. </summary>
    public abstract class Decorable
    {
        /// <summary> Change the behavior. </summary>
        /// <value>Decorator component</value>
        public IDecorator Decorator { get; set; }

        /// <summary> Perform the operation if there is an associated behavior. </summary>
        public void Operation() => Decorator?.Operation();
    }

    /// <summary> It allows alter dynamically behaviors. </summary>
    public abstract class Decorable<R>
    {
        /// <summary> Change the behavior. </summary>
        /// <value>Decorator component</value>
        public IDecorator<R> Decorator { get; set; }

        /// <summary> Perform the operation if there is an associated behavior. </summary>
        /// <value>Value</value>
        public R Operation() => Decorator != null ? Decorator.Operation() : default;
    }

    /// <summary> It allows alter dynamically behaviors. </summary>
    public abstract class Decorable<R, T>
    {
        /// <summary> Change the behavior. </summary>
        /// <value>Decorator component</value>
        public IDecorator<R, T> Decorator { get; set; }

        /// <summary> Perform the operation if there is an associated behavior. </summary>
        /// <param name="value">Value</param>
        /// <returns>Value</returns>
        public R Operation(T value) => Decorator != null ? Decorator.Operation(value) : default;
    }

    /// <summary> It allows alter dynamically behaviors. </summary>
    public abstract class Decorable<R, T0, T1>
    {
        /// <summary> Change the behavior. </summary>
        /// <value>Decorator component</value>
        public IDecorator<R, T0, T1> Decorator { get; set; }

        /// <summary> Perform the operation if there is an associated behavior. </summary>
        /// <param name="value0">First value</param>
        /// <param name="value1">Second value</param>
        /// <returns>Value</returns>
        public R Operation(T0 value0, T1 value1) => Decorator != null ? Decorator.Operation(value0, value1) : default;
    }
}