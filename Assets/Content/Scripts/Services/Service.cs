using Content.Scripts.Configs;
using Content.Scripts.States;
using VContainer;

namespace Content.Scripts.Services
{
    public abstract class Service<TConfig, TState> : Service
        where TConfig : Config
        where TState : State
    {
        public TConfig Config { get; private set; }
        public TState State { get; private set; }
        
        [Inject]
        public void Inject(TConfig config, TState state)
        {
            Config = config;
            State = state;
        }
    }

    public abstract class Service
    {
        public virtual void Init() {}
    }
}