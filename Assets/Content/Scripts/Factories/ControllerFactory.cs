using System.Linq;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class ControllerFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ControllersConfig _controllersConfig;

        public ControllerFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public T Create<T>() where T : Controller, new()
        {
            var prefab = _controllersConfig.Controllers.First(c => c is T);
            var controller = Object.Instantiate(prefab);
            controller.Init();
            _objectResolver.Inject(controller);
            return controller as T;
        }
    }
}