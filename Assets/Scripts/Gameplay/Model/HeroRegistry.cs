using System;
using System.Collections.Generic;
using Gameplay.Data;
using Gameplay.Extensions;
using UI;

namespace Gameplay.Model
{
    public sealed class HeroRegistry
    {
        private readonly Dictionary<TeamColor, List<Entity>> _teams = new();
        private readonly Dictionary<HeroView, Entity> _byView = new();
        private readonly Dictionary<TeamColor, int> _pointers = new();

        public void Register(Entity hero)
        {
            var team = hero.Get<TeamComponent>().Value;
            if (!_teams.ContainsKey(team))
            {
                _teams.Add(team, new());
            }
            if (!_teams[team].Contains(hero))
            {
                _teams[team].Add(hero);
            }

            var view = hero.Get<ViewComponent>().Value;
            _byView[view] = hero;
        }

        public Entity GetNextAlive(TeamColor team)
        {
            var list = _teams[team];
            _pointers.TryGetValue(team, out var start);

            for (var i = 0; i < list.Count; i++)
            {
                var idx = (start + i) % list.Count;
                if (!list[idx].IsAlive()) continue;

                _pointers[team] = idx + 1;
                return list[idx];
            }
            return null;
        }

        public List<Entity> GetAliveTeam(TeamColor team)
        {
            if (!_teams.ContainsKey(team))
            {
                throw new Exception($"{typeof(HeroRegistry)}: registry doesnt contain {team}.");
            }

            var alives = new List<Entity>();
            foreach (var e in _teams[team])
            {
                if (!e.IsAlive()) continue;
                alives.Add(e);
            }

            return alives;
        }

        public Entity GetByView(HeroView view)
        {
            if (!_byView.ContainsKey(view))
            {
                throw new Exception($"{typeof(HeroRegistry)}: registry doesnt contain {view}.");
            }
            return _byView[view];
        }

        public bool IsTeamDefeated(TeamColor team)
        {
            return GetAliveTeam(team).Count == 0;
        }
    }
}