using Gameplay.Extensions;
using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class AttackEventHandler : EventHandler<AttackEvent>
    {
        public AttackEventHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(AttackEvent evt)
        {
            EventBus.RaiseEvent(new DamageRequestEvent(evt.Attacker.Get<AttackDamage>().Value, evt.Target, evt.Attacker));
            EventBus.RaiseEvent(new HitEvent(evt.Attacker, evt.Target));
            EventBus.RaiseEvent(new BackStrikeEvent(evt.Target, evt.Attacker));
            if (evt.Attacker.IsAlive())
            {
                EventBus.RaiseEvent(new AfterAttackEvent(evt.Attacker, evt.Target));
            }
        }
    }
}