using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Pipelines;
using UI;

namespace Gameplay.Tasks.Visuals
{
    public sealed class RedrawHeroOutlineTask : BaseTask
    {
        private readonly HeroView _view;
        private readonly bool _toShow;

        public RedrawHeroOutlineTask(HeroView view, bool toShow)
        {
            _view = view;
            _toShow = toShow;
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            _view.SetActive(_toShow);
            return UniTask.CompletedTask;
        }
    }
}