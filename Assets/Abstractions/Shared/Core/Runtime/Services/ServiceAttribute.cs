using System;

namespace Assets.Abstractions.Shared.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        public Type ServiceType { get; private set; }

        public ServiceAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }
    }
}