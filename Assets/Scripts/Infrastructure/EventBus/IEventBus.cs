using System;

namespace Infrastructure.EventBus
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> handler);
        void Unsubscribe<T>(Action<T> handler);
        void RaiseEvent<T>(T evt);
    }
}