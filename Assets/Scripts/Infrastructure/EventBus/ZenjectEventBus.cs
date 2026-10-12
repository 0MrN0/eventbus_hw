using System;
using Zenject;

namespace Infrastructure.EventBus
{
    public sealed class ZenjectEventBus : IEventBus
    {
        private readonly SignalBus _bus;

        public ZenjectEventBus(SignalBus bus)
        {
            _bus = bus;
        }

        public void RaiseEvent<T>(T evt)
        {
            _bus.Fire(evt);
        }

        public void Subscribe<T>(Action<T> handler)
        {
            _bus.Subscribe(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            _bus.Unsubscribe(handler);
        }
    }
}