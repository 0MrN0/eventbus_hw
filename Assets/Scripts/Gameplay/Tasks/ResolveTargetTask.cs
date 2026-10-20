using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Random;
using Infrastructure.Pipelines;

namespace Gameplay.Tasks
{
    public sealed class ResolveTargetTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly IRandomService _random;
        private readonly HeroRegistry _registry;

        public ResolveTargetTask(TurnContext turnContext, IRandomService random, HeroRegistry registry)
        {
            _turnContext = turnContext;
            _random = random;
            _registry = registry;
        }


        public override UniTask Run(CancellationToken cancelToken)
        {
            if (_turnContext.SkipTurn) return UniTask.CompletedTask;

            if (!_turnContext.Attacker.TryGet<MissAbility>(out var ability)
                    || !_random.Roll(ability.Probability))
            {
                return UniTask.CompletedTask;
            }

            var oppositeTeam = _turnContext.Attacker.Get<TeamComponent>().Value.Opposite();
            var possibleTargets = _registry.GetAliveTeam(oppositeTeam);
            possibleTargets.Remove(_turnContext.Target);

            if (possibleTargets.Count == 0)
            {
                _turnContext.Target = null;
            }
            else
            {
                _turnContext.Target = _random.PickRandom(possibleTargets, 1)[0];
            }

            return UniTask.CompletedTask;
        }
    }
}