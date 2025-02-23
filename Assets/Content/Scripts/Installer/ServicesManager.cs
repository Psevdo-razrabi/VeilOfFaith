using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Services;
using VContainer;

namespace Content.Scripts.Installer
{
    public class ServicesManager
    {
        private IReadOnlyList<IService> _services;
        private IReadOnlyList<IInitializable> _initializableServices;
        private IReadOnlyList<ITickable> _tickableServices;

        [Inject]
        public void Inject(IReadOnlyList<IService> services)
        {
            _services = services;
            _initializableServices = _services.OfType<IInitializable>().ToList();
            _tickableServices = _services.OfType<ITickable>().ToList();
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