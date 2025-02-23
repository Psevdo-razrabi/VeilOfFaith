using Content.Scripts.Configs;
using R3;

namespace Content.Scripts.Services
{
    public class PlayerService : Service<PlayerConfig, PlayerState>, ITickable
    {
        public ReactiveCommand Ticked { get; } = new();

        public void Tick()
        {
            Ticked.Execute();
        }
    }
}