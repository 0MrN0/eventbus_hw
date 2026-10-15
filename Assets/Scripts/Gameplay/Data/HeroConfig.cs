using Gameplay.Model;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Configs/Hero")]
    public sealed class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public AttackDamage AttackDamage { get; private set; }
    }
}