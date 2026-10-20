using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Data;
using Gameplay.Extensions;
using Gameplay.Model;

namespace Gameplay.Enemy
{
    public sealed class SillyEnemyAi : IEnemyAi
    {
        private readonly HeroRegistry _registry;
        private readonly GameConfig _deckConfig;

        public SillyEnemyAi(HeroRegistry registry, GameConfig deckConfig)
        {
            _registry = registry;
            _deckConfig = deckConfig;
        }

        public async UniTask<Entity> SelectTarget(Entity attacker, CancellationToken cancelToken)
        {
            await UniTask.Delay(_deckConfig.EnemyThinkDelayMs, cancellationToken: cancelToken);
            return _registry.GetAliveTeam(attacker.GetTeam().Opposite())[0];
        }
    }
}
