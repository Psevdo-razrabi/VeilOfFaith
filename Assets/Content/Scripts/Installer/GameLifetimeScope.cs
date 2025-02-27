using System.Linq;
using System.Reflection;
using Content.Scripts.Configs;
using Content.Scripts.Factories;
using Content.Scripts.Services;
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
            RegisterFactories();
            RegisterServices();
        }

        private void RegisterConfigs()
        {
            _builder.Register<NullConfig>(Lifetime.Singleton);
            
            var configs = Addressables.LoadAssetsAsync<Config>(_configsAssetLabel, null).WaitForCompletion().ToList();
            foreach (var config in configs)
            {
                Register(config);
            }
        }
        
        private void RegisterFactories()
        {
            Register<ViewModelFactory>();
            Register<ViewFactory>();
            Register<EntityFactory>();
        }

        private void RegisterServices()
        {
            Register<ViewsService>();
            Register<ScenesService>();
            Register<PlayerService>();
            Register<SavingService>();
            Register<InputService>();
        }
        
        private void Register<T>() where T : class
        {
            _builder.Register<T>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
        
        private void Register<T>(T instance) where T : class
        {
            _builder.RegisterInstance(instance).AsImplementedInterfaces().AsSelf();
        }
    }
}