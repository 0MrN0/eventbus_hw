using System;
using Zenject;

namespace Infrastructure.EventBus
{
    public abstract class EventHandler<T> : IInitializable, IDisposable
    {
        protected readonly IEventBus EventBus;
        protected EventHandler(IEventBus eventBus) => EventBus = eventBus;
        public void Initialize() => EventBus.Subscribe<T>(Handle);
        public void Dispose() => EventBus.Unsubscribe<T>(Handle);
        protected abstract void Handle(T evt);
    }

}