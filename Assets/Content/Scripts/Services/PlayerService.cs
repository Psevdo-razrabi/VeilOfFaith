using Content.Scripts.Configs;
using Content.Scripts.Factories;

namespace Content.Scripts.Services
{
    public class PlayerService : Service<NullConfig, PlayerState>
    {
        private ViewFactory _viewFactory;
        
        public PlayerService(ViewFactory viewFactory, ScenesService scenesService)
        {
            _viewFactory = viewFactory;
        }
    }
}