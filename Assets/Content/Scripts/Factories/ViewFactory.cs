using Content.Scripts.Configs;
using Content.Scripts.MVVM;
using Content.Scripts.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Factories
{
    public class ViewFactory : Factory<ViewsConfig>
    {
        [Inject] private ViewModelFactory _viewModelFactory;
        
        private Transform _parent;

        public void Initialize()
        {
            _parent = Object.Instantiate(Config.Canvas, null).transform;
        }

        public async UniTask<T> Create<T>() where T : View
        {
            var prefab = await Config.Load<T>();
            var view = Object.Instantiate(prefab, _parent);
            view.Inject(_viewModelFactory);
            view.Init();
            return view;
        }
    }
}
