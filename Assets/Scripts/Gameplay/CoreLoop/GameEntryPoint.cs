using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Data;
using Gameplay.Model;
using Gameplay.Tasks;
using Infrastructure.Pipelines;
using Zenject;

namespace Gameplay.CoreLoop
{
    public sealed class GameEntryPoint : IInitializable, IDisposable
    {
        private readonly TurnPipeline _turnPipeline;
        private readonly TurnContext _turnContext;
        private readonly HeroSpawner _spawner;

        private readonly StartTurnTask _startTurnTask;
        private readonly SelectTargetTask _selectTargetTask;
        private readonly AttackTask _attackTask;
        private readonly EndTurnTask _endTurnTask;
        private readonly CancellationTokenSource _cts = new();

        public GameEntryPoint(TurnPipeline turnPipeline,
                                TurnContext turnContext,
                                HeroSpawner spawner,
                                StartTurnTask startTurnTask,
                                SelectTargetTask selectTargetTask,
                                AttackTask attackTask,
                                EndTurnTask endTurnTask)
        {
            _turnPipeline = turnPipeline;
            _turnContext = turnContext;
            _spawner = spawner;
            _startTurnTask = startTurnTask;
            _selectTargetTask = selectTargetTask;
            _attackTask = attackTask;
            _endTurnTask = endTurnTask;
        }

        public void Initialize()
        {
            _spawner.Spawn();
            _turnContext.ActiveTeam = _turnContext.PlayerTeam;

            _turnPipeline.AddTask(_startTurnTask);
            _turnPipeline.AddTask(_selectTargetTask);
            _turnPipeline.AddTask(_attackTask);
            _turnPipeline.AddTask(_endTurnTask);

            _turnPipeline.Run(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}