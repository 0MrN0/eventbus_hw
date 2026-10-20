using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Gameplay.Targeting;
using Infrastructure.Pipelines;

namespace Gameplay.Tasks
{
    public sealed class SelectTargetTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly TargetSelectorProvider _selectors;

        public SelectTargetTask(TurnContext turnContext, TargetSelectorProvider selectors)
        {
            _turnContext = turnContext;
            _selectors = selectors;
        }

        public override async UniTask Run(CancellationToken cancelToken)
        {
            if (_turnContext.SkipTurn) return;

            var selector = _selectors.GetFor(_turnContext.ActiveTeam);
            _turnContext.Target = await selector.SelectTarget(_turnContext.Attacker, cancelToken);
        }
    }
}
