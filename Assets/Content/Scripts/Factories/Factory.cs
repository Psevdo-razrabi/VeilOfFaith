using Content.Scripts.Configs;
using VContainer;

namespace Content.Scripts.Factories
{
    public abstract class Factory<TConfig> : Factory
        where TConfig : Config
    {
        [Inject] public TConfig Config { get; set; }
    }
    
    public abstract class Factory {}
}