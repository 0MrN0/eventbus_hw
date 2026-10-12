using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Pipelines
{
    public abstract class BasePipeline
    {
        public event Action Completed;
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
            Debug.Log($"Pipeline {GetType().Name} started.");
        }

        private void OnCompleted()
        {
            Debug.Log($"Pipeline {GetType().Name} completed.");
            Completed?.Invoke();
        }
    }
}