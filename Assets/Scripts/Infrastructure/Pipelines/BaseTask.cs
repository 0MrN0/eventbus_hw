using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Pipelines
{
    public abstract class BaseTask
    {
        public abstract UniTask Run(CancellationToken cancelToken);
    }
}