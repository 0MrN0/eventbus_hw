using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Data;
using Gameplay.Events;
using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Tasks.Visuals;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Tasks
{
    public sealed class EndTurnTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly VisualPipeline _visualPipeline;
        private readonly TurnPipeline _turnPipeline;
        private readonly IEventBus _eventBus;
        private readonly HeroRegistry _registry;

        public EndTurnTask(TurnContext turnContext, VisualPipeline visualPipeline, TurnPipeline turnPipeline, HeroRegistry registry, IEventBus eventBus)
        {
            _turnContext = turnContext;
            _visualPipeline = visualPipeline;
            _turnPipeline = turnPipeline;
            _registry = registry;
            _eventBus = eventBus;
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            if (_turnContext.Attacker != null)
            {
                _visualPipeline.AddTask(new RedrawHeroOutlineTask(_turnContext.Attacker.GetView(), false));
            }

            var activeDefeated = _registry.IsTeamDefeated(_turnContext.ActiveTeam);
            var enemyDefeated = _registry.IsTeamDefeated(_turnContext.OppositeTeam);

            if (activeDefeated || enemyDefeated)
            {
                TeamColor? winner = activeDefeated && enemyDefeated ? null
                                  : activeDefeated ? _turnContext.OppositeTeam
                                  : _turnContext.ActiveTeam;

                _eventBus.RaiseEvent(new GameOverEvent(winner));
                _turnPipeline.Stop();
                return UniTask.CompletedTask;
            }

            _turnContext.PassTurn();
            return UniTask.CompletedTask;
        }
    }
}