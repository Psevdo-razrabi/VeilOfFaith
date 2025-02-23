using System.Numerics;
using Content.Scripts.Configs;
using Content.Scripts.States;
using R3;

namespace Content.Scripts.Services
{
    public class InputService : Service<NullConfig, NullState>, IInitializable, ITickable
    {
        private InputActions _inputActions;
        
        public InputActions.GameplayActions Actions => _inputActions.Gameplay;
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public ReactiveCommand Interacted { get; } = new();
        public ReactiveCommand Ticked { get; } = new();
        
        public void Init()
        {
            _inputActions = new();
            _inputActions.Enable();

            Actions.Interact.performed += _ => Interacted.Execute();
        }

        public void Tick()
        {
            Ticked.Execute();
            
            MoveInput = Actions.Move.ReadValue<Vector2>();
            LookInput = Actions.Look.ReadValue<Vector2>();
        }
    }
}