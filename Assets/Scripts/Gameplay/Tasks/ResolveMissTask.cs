using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Model;
using Gameplay.Random;
using Infrastructure.Pipelines;
using UnityEngine;

namespace Gameplay.Tasks
{
    public sealed class ResolveMissTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly IRandomService _random;
        private readonly HeroRegistry _registry;

        public ResolveMissTask(TurnContext turnContext, IRandomService random, HeroRegistry registry)
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
                Debug.Log($"{_turnContext.Attacker.Get<NameComponent>().Value} missed with no new target. Attack skipped.");
            }
            else
            {
                _turnContext.Target = _random.PickRandom(possibleTargets, 1)[0];
                Debug.Log($"{_turnContext.Attacker.Get<NameComponent>().Value} missed and have new target: {_turnContext.Target.Get<NameComponent>().Value}.");
            }

            return UniTask.CompletedTask;
        }
    }
}