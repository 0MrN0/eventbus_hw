using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Pipelines
{
    public sealed class VisualPipeline : BasePipeline
    {
        public override async UniTask Run(CancellationToken cancelToken)
        {
            await base.Run(cancelToken);
            Clear();
        }
    }
}