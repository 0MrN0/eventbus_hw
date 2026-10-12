using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UI;
using Zenject;

namespace Gameplay.Input
{
    public sealed class HeroClickInput : IInitializable, IDisposable
    {
        private readonly UIService _uiService;
        private UniTaskCompletionSource<HeroView> _clickSource;
        private HeroListView _expectedList;

        public HeroClickInput(UIService uiService)
        {
            _uiService = uiService;
        }

        public void Initialize()
        {
            _uiService.GetBluePlayer().OnHeroClicked += HandleBlueHeroClicked;
            _uiService.GetRedPlayer().OnHeroClicked += HandleRedHeroClicked;
        }

        public UniTask<HeroView> WaitForClick(HeroListView list, CancellationToken token)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            _expectedList = list;
            _clickSource = new();
            return _clickSource.Task.AttachExternalCancellation(token);
        }

        public void Dispose()
        {
            _uiService.GetBluePlayer().OnHeroClicked -= HandleBlueHeroClicked;
            _uiService.GetRedPlayer().OnHeroClicked -= HandleRedHeroClicked;
        }

        private void HandleBlueHeroClicked(HeroView view)
        {
            TryComplete(_uiService.GetBluePlayer(), view);
        }

        private void HandleRedHeroClicked(HeroView view)
        {
            TryComplete(_uiService.GetRedPlayer(), view);
        }

        private void TryComplete(HeroListView heroListView, HeroView view)
        {
            if (_clickSource == null || heroListView != _expectedList)
            {
                return;
            }

            var source = _clickSource;
            _clickSource = null;
            source.TrySetResult(view);
        }
    }
}