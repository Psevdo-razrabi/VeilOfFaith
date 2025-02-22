using System.Collections.Generic;
using System.Linq;
using Content.Scripts.Services;
using VContainer;

namespace Content.Scripts.Installer
{
    public class ServicesManager
    {
        public readonly IReadOnlyList<Service> Services;
        private readonly IReadOnlyList<ITickable> _tickableServices;
        
        public ServicesManager(IObjectResolver objectResolver)
        {
            Services = objectResolver.Resolve<IReadOnlyList<Service>>();
            _tickableServices = Services.OfType<ITickable>().ToList();
            
            foreach (var service in Services.OfType<IInitializable>())
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