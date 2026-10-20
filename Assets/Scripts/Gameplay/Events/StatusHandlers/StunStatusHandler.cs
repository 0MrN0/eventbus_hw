using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class StunStatusHandler : EventHandler<TurnStartedEvent>
    {
        private readonly TurnContext _turnContext;

        public StunStatusHandler(IEventBus eventBus, TurnContext turnContext) : base(eventBus)
        {
            _turnContext = turnContext;
        }

        protected override void Handle(TurnStartedEvent evt)
        {
            if (evt.Attacker.TryGet<StunStatus>(out var ability))
            {
                _turnContext.SkipTurn = true;
                ability.TurnsLeft--;
                evt.Attacker.Set(ability);
                if (ability.TurnsLeft <= 0)
                {
                    evt.Attacker.Remove<StunStatus>();
                }
            }
        }
    }
}