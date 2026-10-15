using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Events;
using Gameplay.Model;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;

namespace Gameplay.Tasks
{
    public sealed class AttackTask : BaseTask
    {
        private readonly TurnContext _turnContext;
        private readonly IEventBus _eventBus;

        public AttackTask(TurnContext context, IEventBus eventBus)
        {
            _turnContext = context;
            _eventBus = eventBus;
        }

        public override UniTask Run(CancellationToken cancelToken)
        {
            if (_turnContext.Target == null)
            {
                return UniTask.CompletedTask;
            }

            _eventBus.RaiseEvent(new AttackEvent(_turnContext.Attacker, _turnContext.Target));
            return UniTask.CompletedTask;
        }
    }
}