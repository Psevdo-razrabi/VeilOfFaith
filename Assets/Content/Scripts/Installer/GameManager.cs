using Content.Scripts.Controllers;
using Content.Scripts.Factories;
using Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Installer
{
    public class GameManager : MonoBehaviour
    {
        private ServicesManager _servicesManager = new();
        [SerializeField] private GameLifetimeScope _lifetimeScope;
        
        private void Awake()
        {
            JsonProjectSettings.ApplyProjectSerializationSettings();
            
            _lifetimeScope.Container.Inject(_servicesManager);
            
            _servicesManager.Awake();
            
            var viewsService = _lifetimeScope.Container.Resolve<ViewsService>();
            viewsService.Initialize();
            
            var controllerFactory = _lifetimeScope.Container.Resolve<ControllerFactory>();
            controllerFactory.Create<PlayerController>();
        }

        private void Update()
        {
            _servicesManager.Update();
            
            // if (Input.GetKeyDown(KeyCode.S))
            // {
            //     var savingService = _lifetimeScope.Container.Resolve<SavingService>();
            //     savingService.Save();
            // }
            //
            // if (Input.GetKeyDown(KeyCode.L))
            // {
            //     var savingService = _lifetimeScope.Container.Resolve<SavingService>();
            //     savingService.Load();
            // }
        }
    }
}