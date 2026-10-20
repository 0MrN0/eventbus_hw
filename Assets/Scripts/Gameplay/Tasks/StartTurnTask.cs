using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Tasks.Visuals;
using Infrastructure.Pipelines;
using UI;

namespace Gameplay.Tasks
{
    public sealed class StartTurnTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly UIService _uiService;
        private readonly HeroRegistry _registry;
        private readonly VisualPipeline _visualPipeline;

        public StartTurnTask(TurnContext turnContext,
                             UIService uiService,
                             HeroRegistry registry,
                             VisualPipeline visualPipeline)
        {
            _turnContext = turnContext;
            _uiService = uiService;
            _registry = registry;
            _visualPipeline = visualPipeline;
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            var attacker = _registry.GetNextAlive(_turnContext.ActiveTeam)
                                ?? throw new System.Exception($"{nameof(StartTurnTask)}: no alive heroes in {_turnContext.ActiveTeam}");
            _turnContext.Attacker = attacker;
            _turnContext.Target = null;
            _turnContext.SkipTurn = false;

            if (_turnContext.Attacker.TryGet<StunStatus>(out var ability))
            {
                _turnContext.SkipTurn = true;
                ability.TurnsLeft--;
                _turnContext.Attacker.Set(ability);
                if (ability.TurnsLeft <= 0)
                {
                    _turnContext.Attacker.Remove<StunStatus>();
                }
            }

            _visualPipeline.AddTask(new RedrawHeroOutlineTask(attacker.GetView(), true));
            _uiService.GetListViewByTeam(_turnContext.ActiveTeam).SetActive(true);
            _uiService.GetListViewByTeam(_turnContext.OppositeTeam).SetActive(false);

            return UniTask.CompletedTask;
        }
    }
}