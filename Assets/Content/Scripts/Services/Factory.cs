using UnityEngine;
using VContainer;

namespace Content.Scripts.Services
{
    public abstract class Factory<TConfig> where TConfig : ScriptableObject
    {
        [Inject] protected TConfig Config;
    }
}