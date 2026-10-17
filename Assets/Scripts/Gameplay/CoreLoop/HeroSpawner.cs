using System;
using Gameplay.Data;
using Gameplay.Extensions;
using Gameplay.Model;
using UI;

namespace Gameplay.CoreLoop
{
    public sealed class HeroSpawner
    {
        private readonly HeroRegistry _registry;
        private readonly UIService _uiService;
        private readonly DeckConfig _deckConfig;

        public HeroSpawner(HeroRegistry registry, UIService uiService, DeckConfig deckConfig)
        {
            _registry = registry;
            _uiService = uiService;
            _deckConfig = deckConfig;
        }

        public void Spawn()
        {
            Spawn(TeamColor.Blue, _deckConfig.Blue, _uiService.GetBluePlayer());
            Spawn(TeamColor.Red, _deckConfig.Red, _uiService.GetRedPlayer());
        }

        private void Spawn(TeamColor color, HeroConfig[] configs, HeroListView viewList)
        {
            if (configs.Length != viewList.GetViews().Count)
            {
                throw new Exception($"{typeof(HeroSpawner)}. {color}'s configs.Length must be equal to {typeof(HeroListView)} list's count.");
            }

            for (var i = 0; i < configs.Length; i++)
            {
                var hero = new Entity();
                var view = viewList.GetView(i);

                hero.Set(new ViewComponent { Value = view });
                hero.Set(new TeamComponent { Value = color });
                hero.Set(new Health { Max = configs[i].Health.Max, Current = configs[i].Health.Max });
                hero.Set(new AttackDamage { Value = configs[i].AttackDamage.Value });

                foreach (var provider in configs[i].Abilities)
                {
                    provider?.Install(hero);
                }

                view.SetIcon(configs[i].Icon);
                view.SetStats(hero.GetStatsText());
                view.SetActive(false);

                _registry.Register(hero);
            }
        }
    }
}