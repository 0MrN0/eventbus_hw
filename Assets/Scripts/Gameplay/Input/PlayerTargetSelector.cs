using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Targeting;
using UI;

namespace Gameplay.Input
{
    public sealed class PlayerTargetSelector : ITargetSelector
    {
        private readonly HeroClickInput _input;
        private readonly HeroRegistry _registry;
        private readonly UIService _uiService;

        public PlayerTargetSelector(HeroClickInput input, HeroRegistry registry, UIService uiService)
        {
            _input = input;
            _registry = registry;
            _uiService = uiService;
        }

        public async UniTask<Entity> SelectTarget(Entity attacker, CancellationToken cancelToken)
        {
            var enemyList = _uiService.GetListViewByTeam(attacker.GetTeam().Opposite());
            Entity target;
            do
            {
                var view = await _input.WaitForClick(enemyList, cancelToken);
                target = _registry.GetByView(view);
            } while (!target.IsAlive());

            return target;
        }
    }
}
