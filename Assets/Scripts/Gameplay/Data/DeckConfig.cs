using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Configs/Deck")]
    public sealed class DeckConfig : ScriptableObject
    {
        public TeamColor PlayerColor;
        public HeroConfig[] Blue;
        public HeroConfig[] Red;
    }
}