using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Input;
using Gameplay.Model;
using Gameplay.Enemy;
using Infrastructure.Pipelines;
using UI;

namespace Gameplay.Tasks
{
    public sealed class SelectTargetTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly HeroClickInput _input;
        private readonly HeroRegistry _registry;
        private readonly UIService _uiService;
        private readonly IEnemyAi _enemy;

        public SelectTargetTask(TurnContext turnContext, HeroClickInput input,
                                HeroRegistry registry, UIService uiService, IEnemyAi enemy)
        {
            _turnContext = turnContext;
            _input = input;
            _registry = registry;
            _uiService = uiService;
            _enemy = enemy;
        }

        public override async UniTask Run(CancellationToken cancelToken)
        {
            if (_turnContext.SkipTurn) return;

            if (!_turnContext.IsPlayerTurn)
            {
                _turnContext.Target = _enemy.ChooseTarget();
                return;
            }

            var enemyList = _uiService.GetListViewByTeam(_turnContext.OppositeTeam);
            Entity target;
            do
            {
                var view = await _input.WaitForClick(enemyList, cancelToken);
                target = _registry.GetByView(view);
            } while (!target.IsAlive());

            _turnContext.Target = target;
        }
    }
}