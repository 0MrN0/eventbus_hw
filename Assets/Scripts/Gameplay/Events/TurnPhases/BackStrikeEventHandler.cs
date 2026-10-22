using Gameplay.Extensions;
using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class BackStrikeEventHandler : EventHandler<BackStrikeEvent>
    {
        public BackStrikeEventHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(BackStrikeEvent evt)
        {
            if (!evt.Attacker.IsAlive()) return;
            if (evt.Attacker.Has<StunStatus>()) return;
            if (evt.Target.Has<BackStrikeImmunAbility>())
            {
                EventBus.RaiseEvent(new AbilityTriggeredEvent(evt.Target));
                return;
            }

            EventBus.RaiseEvent(new DamageRequestEvent(evt.Attacker.Get<AttackDamage>().Value, evt.Target, evt.Attacker));
        }
    }
}