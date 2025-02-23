using Content.Scripts.Configs;
using Content.Scripts.Controllers;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class ControllerFactory : Factory<ControllersConfig>
    {
        private readonly IObjectResolver _objectResolver;

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