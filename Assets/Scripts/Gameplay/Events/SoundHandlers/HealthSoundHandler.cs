using Gameplay.Model;
using Gameplay.Sound;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Events
{
    public sealed class HealthSoundHandler : EventHandler<HealthChangedEvent>
    {
        private const float LowHealthThreshold = 0.2f;

        private readonly VisualPipeline _visualPipeline;
        private readonly IAudioService _audio;

        public HealthSoundHandler(IEventBus eventBus, VisualPipeline visualPipeline, IAudioService audio) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audio = audio;
        }

        protected override void Handle(HealthChangedEvent evt)
        {
            if (evt.HealthBefore == evt.HealthAfter) return;
            if (!evt.Target.TryGet<SoundsComponent>(out var sounds)) return;

            if (evt.HealthAfter <= 0)
            {
                _visualPipeline.AddTask(new PlaySoundTask(_audio, sounds.Value.Death));
                return;
            }

            if (evt.HealthAfter < evt.Target.Get<Health>().Max * LowHealthThreshold)
            {
                _visualPipeline.AddTask(new PlaySoundTask(_audio, sounds.Value.LowHealth));
            }
        }
    }
}
