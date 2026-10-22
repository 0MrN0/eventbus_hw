using Gameplay.Model;
using Gameplay.Sound;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Events
{
    public sealed class AbilitySoundHandler : EventHandler<AbilityTriggeredEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly IAudioService _audio;

        public AbilitySoundHandler(IEventBus eventBus, VisualPipeline visualPipeline, IAudioService audio) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audio = audio;
        }

        protected override void Handle(AbilityTriggeredEvent evt)
        {
            if (!evt.Owner.TryGet<SoundsComponent>(out var sounds)) return;

            _visualPipeline.AddTask(new PlaySoundTask(_audio, sounds.Value.Ability));
        }
    }
}
