using Content.Scripts.Controllers;
using Content.Scripts.Entities;
using Content.Scripts.Factories;
using Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Installer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameLifetimeScope _lifetimeScope;
        
        private ServicesManager _servicesManager = new();
        
        private void Awake()
        {
            JsonProjectSettings.ApplyProjectSerializationSettings();
            
            _lifetimeScope.Container.Inject(_servicesManager);
            
            _servicesManager.Awake();
            
            var viewsService = _lifetimeScope.Container.Resolve<ViewsService>();
            viewsService.Initialize();
            
            var controllerFactory = _lifetimeScope.Container.Resolve<EntityFactory>();
            controllerFactory.CreateControllableEntity<CharacterEntity, PlayerController>();
        }

        private void Update()
        {
            _servicesManager.Update();
            
            if (Input.GetKeyDown(KeyCode.K))
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