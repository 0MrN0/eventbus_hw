using Gameplay.CoreLoop;
using Gameplay.Data;
using Gameplay.Enemy;
using Gameplay.Events;
using Gameplay.Input;
using Gameplay.Model;
using Gameplay.Random;
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
            Container.Bind<IRandomService>().To<RandomService>().AsSingle();
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
            Container.Bind<ResolveMissTask>().AsSingle();
            Container.Bind<AttackTask>().AsSingle();
            Container.Bind<EndTurnTask>().AsSingle();
        }

        private void BindEventBus()
        {
            Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle();
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AttackEvent>();
            Container.DeclareSignal<BackStrikeEvent>();
            Container.DeclareSignal<AfterAttackEvent>();
            Container.DeclareSignal<DealDamageEvent>();
            Container.DeclareSignal<HealEvent>();
            Container.DeclareSignal<GameOverEvent>();
            Container.BindInterfacesTo<AttackEventHandler>().AsSingle();
            Container.BindInterfacesTo<BackStrikeEventHandler>().AsSingle();
            Container.BindInterfacesTo<DealDamageEventHandler>().AsSingle();
            Container.BindInterfacesTo<HealEventHandler>().AsSingle();
            Container.BindInterfacesTo<HealAbilityHandler>().AsSingle();
            Container.BindInterfacesTo<ExtraAttackAbilityHandler>().AsSingle();
            Container.BindInterfacesTo<HealthStealAbilityHandler>().AsSingle();
            Container.BindInterfacesTo<StunAbilityHandler>().AsSingle();
            Container.BindInterfacesTo<GameOverEventHandler>().AsSingle();
        }

        private void BindEnemyAi()
        {
            Container.Bind<IEnemyAi>().To<SillyEnemyAi>().AsSingle();
        }
    }
}