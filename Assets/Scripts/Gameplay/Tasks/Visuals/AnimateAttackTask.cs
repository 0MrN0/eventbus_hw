using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Pipelines;
using UI;

namespace Gameplay.Tasks.Visuals
{
    public sealed class AnimateAttackTask : BaseTask
    {
        private readonly HeroView _attacker;
        private readonly HeroView _target;

        public AnimateAttackTask(HeroView attacker, HeroView target)
        {
            _attacker = attacker;
            _target = target;
        }

        public override UniTask Run(CancellationToken cancelToken) => _attacker.AnimateAttack(_target);
    }
}