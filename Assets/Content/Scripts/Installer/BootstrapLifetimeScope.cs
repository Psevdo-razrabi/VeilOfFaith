using Content.Scripts.Configs;
using Content.Scripts.Factories;
using Content.Scripts.Services;
using Game.MVVM;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Content.Scripts.Installer
{
    public class BootstrapLifetimeScope : LifetimeScope
    {
        [SerializeField] private ViewsConfig _viewsConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterConfigs(builder);
            RegisterFactories(builder);
            RegisterServices(builder);
        }

        private void RegisterConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance(_viewsConfig);
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
        }
    }
}