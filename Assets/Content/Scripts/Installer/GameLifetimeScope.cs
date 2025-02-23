using System.Linq;
using System.Reflection;
using Content.Scripts.Configs;
using Content.Scripts.Factories;
using Content.Scripts.Services;
using Content.Scripts.States;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using VContainer;
using VContainer.Unity;

namespace Content.Scripts.Installer
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AssetLabelReference _configsAssetLabel;

#if UNITY_EDITOR
        [Button("Add scene injectable objects")]
        private void AddSceneInjectableObjectsButton()
        {
            autoInjectGameObjects.Clear();
            
            var gameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (var gameObject in gameObjects)
            {
                var components = gameObject.GetComponents<Component>();

                foreach (var component in components)
                {
                    var type = component.GetType();
                    if(type.GetCustomAttributes<InjectAttribute>(true).Any())
                    {
                        autoInjectGameObjects.Add(component.gameObject);
                    }
                }
            }
        }
#endif

        private IContainerBuilder _builder;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            RegisterConfigs();
            RegisterStates();
            RegisterFactories();
            RegisterServices();
        }

        private void RegisterConfigs()
        {
            _builder.Register<NullConfig>(Lifetime.Singleton);
            
            var configs = Addressables.LoadAssetsAsync<Config>(_configsAssetLabel, null).WaitForCompletion().ToList();
            foreach (var config in configs)
            {
                Reg(config);
            }
        }
        
        private void RegisterStates()
        {
            Reg<NullState>();
            Reg<PlayerState>();
            Reg<DiaryState>();
        }
        
        private void RegisterFactories()
        {
            Reg<ViewModelFactory>();
            Reg<ViewFactory>();
            Reg<ControllerFactory>();
        }

        private void RegisterServices()
        {
            Reg<ViewsService>();
            Reg<ScenesService>();
            Reg<PlayerService>();
            Reg<SavingService>();
            Reg<InputService>();
        }
        
        private void Reg<T>() where T : class
        {
            _builder.Register<T>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
        
        private void Reg<T>(T instance) where T : class
        {
            _builder.RegisterInstance(instance).AsImplementedInterfaces().AsSelf();
        }
    }
}