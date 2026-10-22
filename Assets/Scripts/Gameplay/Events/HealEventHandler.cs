using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;
using UnityEngine;

namespace Gameplay.Events
{
    public sealed class HealEventHandler : EventHandler<HealEvent>
    {
        private readonly VisualPipeline _visualPipeline;

        public HealEventHandler(IEventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void Handle(HealEvent evt)
        {
            if (!evt.Target.IsAlive()) return;

            var health = evt.Target.Get<Health>();
            var healthBefore = health.Current;
            health.Current = Mathf.Min(health.Current + evt.HealAmount, health.Max);
            evt.Target.Set(health);
            _visualPipeline.AddTask(new AnimateAttackTask(evt.Attacker.GetView(), evt.Target.GetView()));
            _visualPipeline.AddTask(new UpdateStatsTask(evt.Target));

            EventBus.RaiseEvent(new HealthChangedEvent(evt.Target, healthBefore, health.Current));
        }
    }
}