using Gameplay.CoreLoop;
using Gameplay.Input;
using Gameplay.Tasks;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;
using UI;
using Zenject;

namespace Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<HeroClickInput>().AsSingle();

            Container.Bind<TurnContext>().AsSingle();

            Container.Bind<VisualPipeline>().AsSingle();
            Container.Bind<TurnPipeline>().AsSingle();

            Container.Bind<SelectTargetTask>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle();

            Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle();
            SignalBusInstaller.Install(Container);

            // Container.DeclareSignal<AttackEvent>();   
        }
    }
}