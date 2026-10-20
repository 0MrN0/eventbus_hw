using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Model;

namespace Gameplay.Targeting
{
    public interface ITargetSelector
    {
        UniTask<Entity> SelectTarget(Entity attacker, CancellationToken cancelToken);
    }
}
