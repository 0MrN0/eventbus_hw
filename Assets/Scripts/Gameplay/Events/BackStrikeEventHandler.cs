using Gameplay.Model;
using Infrastructure.EventBus;
using UnityEngine;

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
                Debug.Log($"{evt.Target.Get<NameComponent>().Value} avoided back strike.");
                return;
            }

            EventBus.RaiseEvent(new DealDamageEvent(evt.Attacker.Get<AttackDamage>().Value, evt.Target, evt.Attacker));
        }
    }
}