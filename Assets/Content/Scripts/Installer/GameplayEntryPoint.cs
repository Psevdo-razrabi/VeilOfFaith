using Content.Scripts.Services;
using Game.DI;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Installer
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayLifetimeScope _lifetimeScope;
        
        private void Awake()
        {
            var viewsService = _lifetimeScope.Container.Resolve<ViewsService>();
            viewsService.Initialize();
        }
    }
}