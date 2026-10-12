using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Tasks;
using Infrastructure.Pipelines;
using UI;
using Zenject;

namespace Gameplay.CoreLoop
{
    public sealed class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly CancellationTokenSource _cts = new();

        public GameEntryPoint(TurnPipeline turnPipeline,
                                SelectTargetTask selectTask,
                                UIService uiService,
                                TurnContext turnContext)
        {
            _turnPipeline = turnPipeline;

            turnContext.ActivePlayer = uiService.GetBluePlayer();
            turnContext.EnemyPlayer = uiService.GetRedPlayer();

            _turnPipeline.AddTask(selectTask);
        }

        public void Initialize()
        {
            _turnPipeline.Run(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}