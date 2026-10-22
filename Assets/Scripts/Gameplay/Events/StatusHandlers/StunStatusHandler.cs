using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class StunStatusHandler : EventHandler<TurnStartedEvent>
    {
        public StunStatusHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(TurnStartedEvent evt)
        {
            if (!evt.Attacker.TryGet<StunStatus>(out var stun)) return;

            evt.SkipTurn = true;
            stun.TurnsLeft--;
            evt.Attacker.Set(stun);
            if (stun.TurnsLeft <= 0)
            {
                evt.Attacker.Remove<StunStatus>();
            }
        }
    }
}