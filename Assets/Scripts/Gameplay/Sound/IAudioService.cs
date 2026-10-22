using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Sound
{
    public interface IAudioService
    {
        UniTask PlayRandomAsync(IReadOnlyList<AudioClip> clips, CancellationToken cancelToken);
    }
}
