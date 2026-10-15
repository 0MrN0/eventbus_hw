using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Events
{
    public sealed class AttackEventHandler : EventHandler<AttackEvent>
    {
        private readonly VisualPipeline _visualPipeline;

        public AttackEventHandler(IEventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void Handle(AttackEvent evt)
        {
            _visualPipeline.AddTask(new AnimateAttackTask(evt.Attacker.GetView(), evt.Target.GetView()));
            EventBus.RaiseEvent(new DealDamageEvent(evt.Attacker.Get<AttackDamage>().Value, evt.Target));
            EventBus.RaiseEvent(new DealDamageEvent(evt.Target.Get<AttackDamage>().Value, evt.Attacker));
        }
    }
}