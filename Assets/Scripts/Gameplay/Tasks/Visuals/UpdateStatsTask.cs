using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Extensions;
using Gameplay.Model;
using Infrastructure.Pipelines;
using UI;

namespace Gameplay.Tasks.Visuals
{
    public sealed class UpdateStatsTask : BaseTask
    {
        private readonly HeroView _view;
        private readonly string _stats;

        public UpdateStatsTask(Entity hero)
        {
            _view = hero.GetView();
            _stats = hero.GetStatsText();
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            _view.SetStats(_stats);
            return UniTask.CompletedTask;
        }
    }
}