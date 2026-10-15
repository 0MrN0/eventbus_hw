using System;
using System.Collections.Generic;

namespace Gameplay.Model
{
    public sealed class Entity
    {
        private readonly Dictionary<Type, object> _components = new();

        public bool TryGet<T>(out T component) where T : struct
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                component = (T)value;
                return true;
            }

            component = default;
            return false;
        }

        public void Set<T>(T component) where T : struct
        {
            _components[typeof(T)] = component;
        }

        public void Remove<T>() where T : struct
        {
            _components.Remove(typeof(T));
        }

        public bool Has<T>() where T : struct
        {
            return _components.ContainsKey(typeof(T));
        }

        public T Get<T>() where T : struct
        {
            return (T)_components[typeof(T)];
        }
    }
}