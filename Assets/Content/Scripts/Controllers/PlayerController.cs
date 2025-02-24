using Content.Scripts.Services;
using R3;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Controllers
{
    public class PlayerController : CharacterController
    {
        private InputService _inputService;
        
        public override Vector2 MoveDirection => _inputService.Actions.Move.ReadValue<Vector2>();
        public override Vector2 LookDirection => _inputService.Actions.Look.ReadValue<Vector2>();
        public override ReactiveCommand Interacted { get; }
        
        [Inject]
        private void Inject(InputService inputService)
        {
            _inputService = inputService;
            
            _inputService.Actions.Interact.performed += _ => Interacted.Execute();
        }
    }
}