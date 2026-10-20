using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class DamageRequestHandler : EventHandler<DamageRequestEvent>
    {
        public DamageRequestHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(DamageRequestEvent evt)
        {
            var before = new BeforeDamageEvent(evt.Damage, evt.Target, evt.Attacker);
            EventBus.RaiseEvent(before);
            EventBus.RaiseEvent(new DealDamageEvent(before.Damage, before.Target, before.Attacker));
        }
    }
}