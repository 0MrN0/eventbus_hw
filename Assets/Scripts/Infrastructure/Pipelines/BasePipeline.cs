using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Pipelines
{
    public abstract class BasePipeline
    {
        public event Action Completed;
        protected IReadOnlyList<BaseTask> Tasks => _tasks;
        private readonly List<BaseTask> _tasks = new();

        public void AddTask(BaseTask task)
        {
            _tasks.Add(task);
        }

        public void Clear()
        {
            _tasks.Clear();
        }

        public virtual async UniTask Run(CancellationToken cancelToken)
        {
            OnStarted();
            for (var i = 0; i < _tasks.Count; i++)
            {
                await _tasks[i].Run(cancelToken);
            }
            OnCompleted();
        }

        private void OnStarted()
        {
        }

        private void OnCompleted()
        {
            Completed?.Invoke();
        }
    }
}