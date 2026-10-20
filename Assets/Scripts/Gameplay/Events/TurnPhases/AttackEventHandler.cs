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
            EventBus.RaiseEvent(new BackStrikeEvent(evt.Target, evt.Attacker));
            EventBus.RaiseEvent(new AfterAttackEvent(evt.Attacker, evt.Target));
        }
    }
}