using Gameplay.Extensions;
using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class StunAbilityHandler : EventHandler<HitEvent>
    {
        public StunAbilityHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(HitEvent evt)
        {
            if (!evt.Target.IsAlive()) return;
            if (!evt.Attacker.TryGet<StunAbility>(out var ability))
            {
                return;
            }

            EventBus.RaiseEvent(new AbilityTriggeredEvent(evt.Attacker));
            evt.Target.Set(new StunStatus { TurnsLeft = ability.Turns });
        }
    }
}