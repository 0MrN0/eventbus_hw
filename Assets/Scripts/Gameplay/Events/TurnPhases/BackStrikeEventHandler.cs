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
            if (evt.Target.Has<BackStrikeImmunAbility>())
            {
                return;
            }

            EventBus.RaiseEvent(new DamageRequestEvent(evt.Attacker.Get<AttackDamage>().Value, evt.Target, evt.Attacker));
        }
    }
}