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

            evt.Damage = 0;
            immune.StrikesLeft--;
            evt.Target.Set(immune);
            if (immune.StrikesLeft <= 0) evt.Target.Remove<ImmuneStatus>();
        }
    }
}