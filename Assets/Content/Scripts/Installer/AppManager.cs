using UnityEngine;
using VContainer;

namespace Content.Scripts.Installer
{
    public class AppManager : MonoBehaviour
    {
        private ServicesManager _servicesManager;

        public void Initialize(IObjectResolver objectResolver)
        {
            _servicesManager = new(objectResolver);
        }

        private void Start()
        {
            _servicesManager.Initialize();
        }

        private void Update()
        {
            _servicesManager.Tick();
        }
    }
}