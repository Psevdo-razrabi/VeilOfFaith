using Content.Scripts.Configs;
using VContainer;
using State = Content.Scripts.States.State;

namespace Content.Scripts.Services
{
    public abstract class Service<TConfig, TState> : Service
        where TConfig : Config
        where TState : State, new()
    {
        public TConfig Config { get; private set; }

        public override State State => new TState();
        
        [Inject]
        private void Inject(TConfig config)
        {
            Config = config;
        }
    }

    public abstract class Service : IService
    {
        public abstract State State { get; }
    }
}