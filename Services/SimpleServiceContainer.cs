using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Project5LMS.Services
{
    /// <summary>
    /// Simple Dependency Injection container that doesn't require external packages
    /// Implements basic DI functionality for the project
    /// </summary>
    public class SimpleServiceContainer
    {
        private readonly Dictionary<Type, Func<object>> _transientRegistrations = new Dictionary<Type, Func<object>>();
        private readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();
        private readonly object _lock = new object();

        /// <summary>
        /// Register a transient service (new instance each time)
        /// </summary>
        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            _transientRegistrations[typeof(TInterface)] = () => CreateInstance<TImplementation>();
        }

        /// <summary>
        /// Register a transient service with a factory function
        /// </summary>
        public void RegisterTransient<T>(Func<T> factory) where T : class
        {
            _transientRegistrations[typeof(T)] = () => factory();
        }

        /// <summary>
        /// Register a singleton service (shared instance)
        /// </summary>
        public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            lock (_lock)
            {
                if (!_singletonInstances.ContainsKey(typeof(TInterface)))
                {
                    _singletonInstances[typeof(TInterface)] = CreateInstance<TImplementation>();
                }
            }
        }

        /// <summary>
        /// Get a service instance
        /// </summary>
        public T GetService<T>() where T : class
        {
            return (T)GetService(typeof(T));
        }

        /// <summary>
        /// Get a service instance (non-generic)
        /// </summary>
        public object GetService(Type serviceType)
        {
            // Check singleton first
            if (_singletonInstances.ContainsKey(serviceType))
            {
                return _singletonInstances[serviceType];
            }

            // Check transient registrations
            if (_transientRegistrations.ContainsKey(serviceType))
            {
                return _transientRegistrations[serviceType]();
            }

            return null;
        }

        /// <summary>
        /// Get a required service (throws if not found)
        /// </summary>
        public T GetRequiredService<T>() where T : class
        {
            var service = GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
            }
            return service;
        }

        /// <summary>
        /// Create an instance of a type with dependency injection
        /// </summary>
        private T CreateInstance<T>() where T : class
        {
            var type = typeof(T);
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            // Find the constructor with the most parameters (usually the one with dependencies)
            var constructor = constructors.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();

            if (constructor == null)
            {
                return Activator.CreateInstance<T>();
            }

            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var paramType = parameters[i].ParameterType;
                args[i] = GetService(paramType);

                if (args[i] == null)
                {
                    throw new InvalidOperationException($"Cannot resolve dependency of type {paramType.Name} for {type.Name}");
                }
            }

            return (T)constructor.Invoke(args);
        }
    }
}

