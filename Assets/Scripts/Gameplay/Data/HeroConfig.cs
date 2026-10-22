using System;
using System.Collections.Generic;
using Gameplay.Model;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(menuName = "Configs/Hero")]
    public sealed class HeroConfig : ScriptableObject
    {
        [Serializable]
        public struct HeroSounds
        {
            public AudioClip[] StartTurn;
            public AudioClip[] Ability;
            public AudioClip LowHealth;
            public AudioClip Death;
        }

        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public Health Health { get; private set; }
        [field: SerializeField] public AttackDamage AttackDamage { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public HeroSounds Sounds { get; private set; }

        [field: SerializeReference, SubclassSelector]
        public List<IComponentProvider> Abilities = new();
    }
}
