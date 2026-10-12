using System.Threading;
using Cysharp.Threading.Tasks;

namespace Infrastructure.Pipelines
{
    public sealed class TurnPipeline : BasePipeline
    {
        private readonly VisualPipeline _visualPipeline;
        private bool _isStopped;

        public TurnPipeline(VisualPipeline visualPipeline)
        {
            _visualPipeline = visualPipeline;
        }

        public void Stop()
        {
            _isStopped = true;
        }

        public override async UniTask Run(CancellationToken cancelToken)
        {
            _isStopped = false;

            while (!_isStopped && !cancelToken.IsCancellationRequested)
            {
                await base.Run(cancelToken);
                await _visualPipeline.Run(cancelToken);
                await UniTask.Yield(PlayerLoopTiming.Update, cancelToken);
            }
        }
    }
}
