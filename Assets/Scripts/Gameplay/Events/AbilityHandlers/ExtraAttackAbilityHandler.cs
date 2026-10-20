using Gameplay.Model;
using Gameplay.Random;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class ExtraAttackAbilityHandler : EventHandler<AfterAttackEvent>
    {
        private readonly IRandomService _random;
        private readonly HeroRegistry _registry;

        public ExtraAttackAbilityHandler(IEventBus eventBus, IRandomService random, HeroRegistry registry) : base(eventBus)
        {
            _random = random;
            _registry = registry;
        }

        protected override void Handle(AfterAttackEvent evt)
        {
            if (!evt.Attacker.TryGet<ExtraAttackAbility>(out var ability)) return;

            var targets = _random.PickRandom(_registry.GetAliveTeam(evt.Target.Get<TeamComponent>().Value), ability.TargetsCount);
            foreach (var target in targets)
            {
                EventBus.RaiseEvent(new DealDamageEvent(ability.Damage, target, evt.Attacker));
            }
        }
    }
}