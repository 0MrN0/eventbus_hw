using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;
using UnityEngine;

namespace Gameplay.Events
{
    public sealed class DealDamageEventHandler : EventHandler<DealDamageEvent>
    {
        private readonly VisualPipeline _visualPipeline;

        public DealDamageEventHandler(IEventBus eventBus, VisualPipeline visualPipeline) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
        }

        protected override void Handle(DealDamageEvent evt)
        {
            _visualPipeline.AddTask(new AnimateAttackTask(evt.Attacker.GetView(), evt.Target.GetView()));
            if (evt.Target.TryGet<ImmuneStatus>(out var immune))
            {
                immune.StrikesLeft -= 1;
                evt.Target.Set(immune);
                if (immune.StrikesLeft <= 0)
                {
                    evt.Target.Remove<ImmuneStatus>();
                }
                return;
            }

            var health = evt.Target.Get<Health>();
            health.Current = Mathf.Max(0, health.Current - evt.Damage);
            evt.Target.Set(health);
            _visualPipeline.AddTask(new UpdateStatsTask(evt.Target));
        }
    }
}