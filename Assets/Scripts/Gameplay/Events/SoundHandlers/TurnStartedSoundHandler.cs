using Gameplay.Model;
using Gameplay.Sound;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Events
{
    public sealed class TurnStartedSoundHandler : EventHandler<TurnStartedEvent>
    {
        private readonly VisualPipeline _visualPipeline;
        private readonly IAudioService _audio;

        public TurnStartedSoundHandler(IEventBus eventBus, VisualPipeline visualPipeline, IAudioService audio) : base(eventBus)
        {
            _visualPipeline = visualPipeline;
            _audio = audio;
        }

        protected override void Handle(TurnStartedEvent evt)
        {
            if (!evt.Attacker.TryGet<SoundsComponent>(out var sounds)) return;

            _visualPipeline.AddTask(new PlaySoundTask(_audio, sounds.Value.StartTurn));
        }
    }
}
