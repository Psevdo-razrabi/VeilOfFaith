using System.Linq;
using Content.Scripts.Configs;
using Content.Scripts.Factories;
using Content.Scripts.Services;
using Content.Scripts.States;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Content.Scripts.Installer
{
    public class BootstrapLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetLabelReference _configsAssetLabel;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigs(builder);
            RegisterStates(builder);
            RegisterFactories(builder);
            RegisterServices(builder);
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.Register<NullConfig>(Lifetime.Singleton);
            
            var configs = Addressables.LoadAssetsAsync<Config>(_configsAssetLabel, null).WaitForCompletion().ToList();
            foreach (var config in configs)
            {
                var configType = config.GetType();
                builder.RegisterInstance(config).As(configType);
            }
        }
        
        private void RegisterStates(IContainerBuilder builder)
        {
            // List<State> states = new()
            // {
            //     new NullState(),
            //     new PlayerState(),
            // };
            //
            // foreach (var state in states)
            // {
            //     var stateType = state.GetType();
            //     builder.RegisterInstance(state).As(stateType);
            // }
            
            builder.Register<NullState>(Lifetime.Singleton);
            builder.Register<PlayerState>(Lifetime.Singleton);
        }
        
        private void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<ViewModelFactory>(Lifetime.Singleton);
            builder.Register<ViewFactory>(Lifetime.Singleton);
            builder.Register<ControllerFactory>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ViewsService>(Lifetime.Singleton);
            builder.Register<ScenesService>(Lifetime.Singleton);
            builder.Register<SavingService>(Lifetime.Singleton);
            builder.Register<PlayerService>(Lifetime.Singleton);
        }
    }
}