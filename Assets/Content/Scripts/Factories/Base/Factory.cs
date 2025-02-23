using Content.Scripts.Configs;
using VContainer;

namespace Content.Scripts.Factories
{
    public abstract class Factory<TConfig> : IFactory
        where TConfig : Config
    {
        [Inject] public TConfig Config { get; set; }
    }
    
    public interface IFactory {}
}