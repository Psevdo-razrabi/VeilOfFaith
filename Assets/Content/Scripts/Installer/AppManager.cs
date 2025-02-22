using Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Installer
{
    public class AppManager : MonoBehaviour
    {
        private ServicesManager _servicesManager;
        private StatesManager _statesManager;
        [SerializeField] private BootstrapLifetimeScope _lifetimeScope;

        private void Awake()
        {
            _servicesManager = new(_lifetimeScope.Container);
            _statesManager = new(_lifetimeScope.Container);

            var savingService = _lifetimeScope.Container.Resolve<SavingService>();
            savingService.InitStates(_statesManager.States);
            
            var viewsService = _lifetimeScope.Container.Resolve<ViewsService>();
            viewsService.Initialize();
        }

        private void Update()
        {
            _servicesManager.Tick();
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                var savingService = _lifetimeScope.Container.Resolve<SavingService>();
                savingService.Save();
            }
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                var savingService = _lifetimeScope.Container.Resolve<SavingService>();
                savingService.Load();
            }
        }
    }
}