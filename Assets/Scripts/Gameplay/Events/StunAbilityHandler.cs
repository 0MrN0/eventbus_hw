using UnityEngine;
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
            Debug.Log($"{evt.Attacker.Get<NameComponent>().Value} stunned {evt.Target.Get<NameComponent>().Value}");
        }
    }
}