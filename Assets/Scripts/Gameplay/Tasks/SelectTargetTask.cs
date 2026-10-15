using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Data;
using Gameplay.Extensions;
using Gameplay.Input;
using Gameplay.Model;
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

        public SelectTargetTask(TurnContext turnContext, HeroClickInput input,
                                HeroRegistry registry, UIService uiService)
        {
            _turnContext = turnContext;
            _input = input;
            _registry = registry;
            _uiService = uiService;
        }

        public override async UniTask Run(CancellationToken cancelToken)
        {
            if (!_turnContext.IsPlayerTurn)
            {
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