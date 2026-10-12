using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.CoreLoop;
using Gameplay.Input;
using Infrastructure.Pipelines;
using UnityEngine;

namespace Gameplay.Tasks
{
    public sealed class SelectTargetTask : BaseTask
    {
        private readonly HeroClickInput _input;
        private readonly TurnContext _context;

        public SelectTargetTask(HeroClickInput input, TurnContext context)
        {
            _input = input;
            _context = context;
        }

        public override async UniTask Run(CancellationToken cancelToken)
        {
            _context.Target = await _input.WaitForClick(_context.EnemyPlayer, cancelToken);
        }
    }
}