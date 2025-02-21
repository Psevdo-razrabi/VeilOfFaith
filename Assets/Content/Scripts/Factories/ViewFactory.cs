using System.Linq;
using Content.Scripts.Configs;
using Content.Scripts.MVVM;
using Cysharp.Threading.Tasks;
using Game.MVVM;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class ViewFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ViewsConfig _viewsConfig;
        private Transform _parent;

        public ViewFactory(IObjectResolver objectResolver, ViewsConfig viewsConfig)
        {
            _objectResolver = objectResolver;
            _viewsConfig = viewsConfig;
        }

        public void Initialize()
        {
            _parent = Object.Instantiate(_viewsConfig.Canvas, null).transform;
        }

        public async UniTask<T> Create<T>() where T : View
        {
            var prefab = await _viewsConfig.Load<T>();
            var view = Object.Instantiate(prefab, _parent);
            _objectResolver.Inject(view);
            return view;
        }
    }
}
