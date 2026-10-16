using Gameplay.CoreLoop;
using Gameplay.Data;
using Gameplay.Enemy;
using Gameplay.Events;
using Gameplay.Input;
using Gameplay.Model;
using Gameplay.Tasks;
using Infrastructure.EventBus;
using Infrastructure.Pipelines;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private DeckConfig _deckConfig;

        public override void InstallBindings()
        {
            BindServices();
            BindConfigs();
            BindPipelines();
            BindEventBus();
            BindEnemyAi();
            Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle();
        }

        private void BindServices()
        {
            Container.Bind<UIService>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroClickInput>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_deckConfig);
            Container.Bind<HeroRegistry>().AsSingle();
            Container.Bind<TurnContext>().AsSingle();
            Container.Bind<HeroSpawner>().AsSingle();
        }

        private void BindPipelines()
        {
            Container.Bind<VisualPipeline>().AsSingle();
            Container.Bind<TurnPipeline>().AsSingle();

            Container.Bind<StartTurnTask>().AsSingle();
            Container.Bind<SelectTargetTask>().AsSingle();
            Container.Bind<AttackTask>().AsSingle();
            Container.Bind<EndTurnTask>().AsSingle();
        }

        private void BindEventBus()
        {
            Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AttackEvent>();
            Container.DeclareSignal<DealDamageEvent>();
            Container.DeclareSignal<GameOverEvent>();
            Container.BindInterfacesTo<AttackEventHandler>().AsSingle();
            Container.BindInterfacesTo<DealDamageEventHandler>().AsSingle();
            Container.BindInterfacesTo<GameOverEventHandler>().AsSingle();
        }

        private void BindEnemyAi()
        {
            Container.Bind<IEnemyAi>().To<SillyEnemyAi>().AsSingle();
        }
    }
}