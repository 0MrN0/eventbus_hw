using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Random;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class HealAbilityHandler : EventHandler<AfterAttackEvent>
    {
        private readonly IRandomService _random;
        private readonly HeroRegistry _registry;

        public HealAbilityHandler(IEventBus eventBus, HeroRegistry registry, IRandomService random) : base(eventBus)
        {
            _registry = registry;
            _random = random;
        }

        protected override void Handle(AfterAttackEvent evt)
        {
            if (!evt.Attacker.TryGet<HealAbility>(out var ability)) return;

            var healCandidates = _registry.GetAliveTeam(evt.Attacker.GetTeam());
            healCandidates.Remove(evt.Attacker);
            if (healCandidates.Count == 0) return;

            var healTargets = _random.PickRandom(healCandidates, ability.TargetsCount);
            foreach (var target in healTargets)
            {
                EventBus.RaiseEvent(new HealEvent(target, ability.Value, evt.Attacker));
            }
        }
    }
}