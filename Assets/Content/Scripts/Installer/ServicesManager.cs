using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Services;
using VContainer;

namespace Content.Scripts.Installer
{
    public class ServicesManager
    {
        private SavingService _savingService;
        private IReadOnlyList<IInitializable> _initializableServices;
        private IReadOnlyList<ITickable> _tickableServices;

        [Inject]
        public void Inject(IReadOnlyList<IService> services, SavingService savingService)
        {
            _savingService = savingService;
            _savingService.States = services.OfType<Service>().Select(s => s.State).ToList();
            
            _initializableServices = services.OfType<IInitializable>().ToList();
            _tickableServices = services.OfType<ITickable>().ToList();
        }
        
        public void Awake()
        {
            foreach (var service in _initializableServices)
            {
                service.Init();
            }
        }
        
        public void Update()
        {
            foreach (var service in _tickableServices)
            {
                service.Tick();
            }
        }
    }
}