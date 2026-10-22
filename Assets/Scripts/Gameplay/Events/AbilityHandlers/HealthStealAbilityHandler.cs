using Gameplay.Model;
using Gameplay.Random;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class HealthStealAbilityHandler : EventHandler<AfterAttackEvent>
    {
        private readonly IRandomService _random;

        public HealthStealAbilityHandler(IEventBus eventBus, IRandomService random) : base(eventBus)
        {
            _random = random;
        }

        protected override void Handle(AfterAttackEvent evt)
        {
            if (!evt.Attacker.TryGet<HealthStealingAbility>(out var ability)
                    || !_random.Roll(ability.Probability))
            {
                return;
            }

            EventBus.RaiseEvent(new AbilityTriggeredEvent(evt.Attacker));
            EventBus.RaiseEvent(new HealEvent(evt.Attacker, evt.Attacker.Get<AttackDamage>().Value, evt.Attacker));
        }
    }
}