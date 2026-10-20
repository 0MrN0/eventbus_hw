using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class StunAbilityHandler : EventHandler<AfterAttackEvent>
    {
        public StunAbilityHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(AfterAttackEvent evt)
        {
            if (!evt.Attacker.TryGet<StunAbility>(out var ability))
            {
                return;
            }

            evt.Target.Set(new StunStatus { TurnsLeft = ability.Turns });
        }
    }
}