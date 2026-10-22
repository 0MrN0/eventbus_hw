using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Sound;
using Infrastructure.Pipelines;
using UnityEngine;

namespace Gameplay.Tasks.Visuals
{
    public sealed class PlaySoundTask : BaseTask
    {
        private readonly IAudioService _audio;
        private readonly IReadOnlyList<AudioClip> _clips;

        public PlaySoundTask(IAudioService audio, params AudioClip[] clips)
        {
            _audio = audio;
            _clips = clips;
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            return _audio.PlayRandomAsync(_clips, cancelToken);
        }
    }
}
