using UnityEngine;

namespace Content.Scripts.Installer
{
    public class BootstrapEntryPoint : MonoBehaviour
    {
        [SerializeField] private BootstrapLifetimeScope _lifetimeScope;
        [SerializeField] private AppManager _appManager;
        
        private void Awake()
        {
            DontDestroyOnLoad(_lifetimeScope);
            _lifetimeScope.Build();

            _appManager.Initialize(_lifetimeScope.Container);
        }
    }
}