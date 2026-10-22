using Gameplay.Model;
using Infrastructure.EventBus;

namespace Gameplay.Events
{
    public sealed class ImmuneStatusHandler : EventHandler<BeforeDamageEvent>
    {
        public ImmuneStatusHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(BeforeDamageEvent evt)
        {
            if (!evt.Target.TryGet<ImmuneStatus>(out var immune)) return;

            EventBus.RaiseEvent(new AbilityTriggeredEvent(evt.Target));
            evt.Damage = 0;
            immune.StrikesLeft--;
            evt.Target.Set(immune);
            if (immune.StrikesLeft <= 0) evt.Target.Remove<ImmuneStatus>();
        }
    }
}