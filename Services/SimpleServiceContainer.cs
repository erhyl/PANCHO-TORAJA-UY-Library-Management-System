using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Project5LMS.Services
{
    public class SimpleServiceContainer
    {
        private readonly Dictionary<Type, Func<object>> _transientRegistrations = new Dictionary<Type, Func<object>>();
        private readonly Dictionary<Type, object> _singletonInstances = new Dictionary<Type, object>();
        private readonly object _lock = new object();
        public void RegisterTransient<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            _transientRegistrations[typeof(TInterface)] = () => CreateInstance<TImplementation>();
        }
        public void RegisterTransient<T>(Func<T> factory) where T : class
        {
            _transientRegistrations[typeof(T)] = () => factory();
        }
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
        public T GetService<T>() where T : class
        {
            return (T)GetService(typeof(T));
        }
        public object GetService(Type serviceType)
        {
            if (_singletonInstances.ContainsKey(serviceType))
            {
                return _singletonInstances[serviceType];
            }
            if (_transientRegistrations.ContainsKey(serviceType))
            {
                return _transientRegistrations[serviceType]();
            }
            return null;
        }
        public T GetRequiredService<T>() where T : class
        {
            var service = GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
            }
            return service;
        }
        private T CreateInstance<T>() where T : class
        {
            var type = typeof(T);
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
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