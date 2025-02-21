using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Services;
using VContainer;

namespace Content.Scripts.Installer
{
    public class ServicesManager
    {
        private readonly IReadOnlyList<IService> _services;
        private readonly IReadOnlyList<ITickable> _tickableServices;
        
        public ServicesManager(IObjectResolver objectResolver)
        {
            _services = objectResolver.Resolve<IReadOnlyList<IService>>();
            _tickableServices = _services.OfType<ITickable>().ToList();
        }
        
        public void Initialize()
        {
            foreach (var service in _services.OfType<IInitializable>())
            {
                service.Initialize();
            }
        }
        
        public void Tick()
        {
            foreach (var service in _tickableServices)
            {
                service.Tick();
            }
        }
    }
}