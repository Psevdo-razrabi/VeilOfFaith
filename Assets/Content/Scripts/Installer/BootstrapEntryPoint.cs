using Content.Scripts.Installer;
using Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Game.DI
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        [SerializeField] private BootstrapLifetimeScope _lifetimeScope;
        
        private void Awake()
        {
            DontDestroyOnLoad(_lifetimeScope);
            _lifetimeScope.Build();
            
            var viewsService = _lifetimeScope.Container.Resolve<ViewsService>();
            viewsService.Initialize();
        }
    }
}