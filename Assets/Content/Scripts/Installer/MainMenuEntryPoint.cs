using Game.Services;
using UnityEngine;
using VContainer;

namespace Game.DI
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private MainMenuLifetimeScope _lifetimeScope;
        
        private void Awake()
        {
        }
    }
}
