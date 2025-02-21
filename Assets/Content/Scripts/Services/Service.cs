using UnityEngine;
using VContainer;

namespace Content.Scripts.Services
{
    public abstract class Service<TConfig> : IService where TConfig : ScriptableObject
    {
        [Inject] protected TConfig Config;
    }

    public interface IService
    {
        
    }

    public interface ITickable
    {
        void Tick();
    }

    public interface IInitializable
    {
        void Initialize();
    }
}