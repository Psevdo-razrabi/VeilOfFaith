using Content.Scripts.Configs;
using Content.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class ControllerFactory : Factory<ControllersConfig>
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ControllersConfig _controllersConfig;

        public ControllerFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public async UniTask<T> Create<T>() where T : Controller, new()
        {
            var prefab = await Config.Load<T>();
            var controller = Object.Instantiate(prefab);
            controller.Init();
            _objectResolver.Inject(controller);
            return controller;
        }
    }
}