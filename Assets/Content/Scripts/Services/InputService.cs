using Content.Scripts.Configs;
using Content.Scripts.States;

namespace Content.Scripts.Services
{
    public class InputService : Service<NullConfig, NullState>, IInitializable
    {
        private InputActions _inputActions;
        
        public InputActions.GameplayActions Actions => _inputActions.Gameplay;
        
        public void Init()
        {
            _inputActions = new();
            _inputActions.Enable();
        }
    }
}