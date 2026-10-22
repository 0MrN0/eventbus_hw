using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Sound
{
    public sealed class AudioService : IAudioService
    {
        private readonly AudioPlayer _player;

        public AudioService(AudioPlayer player)
        {
            _player = player;
        }

        public UniTask PlayRandomAsync(IReadOnlyList<AudioClip> clips, CancellationToken cancelToken)
        {
            if (clips == null || clips.Count == 0) return UniTask.CompletedTask;

            var clip = clips[UnityEngine.Random.Range(0, clips.Count)];
            if (clip == null) return UniTask.CompletedTask;

            _player.PlaySound(clip);
            return UniTask.Delay(TimeSpan.FromSeconds(clip.length), DelayType.Realtime, cancellationToken: cancelToken);
        }
    }
}
