using Infrastructure.EventBus;
using UnityEngine;

namespace Gameplay.Events
{
    public sealed class GameOverEventHandler : EventHandler<GameOverEvent>
    {
        public GameOverEventHandler(IEventBus eventBus) : base(eventBus)
        {
        }

        protected override void Handle(GameOverEvent evt)
        {
            Debug.Log(evt.Winner.HasValue ? $"Game over. Winner: {evt.Winner}" : "Game over. Draw");
        }
    }
}