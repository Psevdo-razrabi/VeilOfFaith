using Content.Scripts.Controllers;
using Content.Scripts.Services;
using UnityEngine;
using VContainer;
using CharacterController = Content.Scripts.Controllers.CharacterController;

namespace Content.Scripts.Entities
{
    public class CharacterEntity : ControllableEntity<CharacterController>
    {
        [SerializeField] private Camera _camera;
    
        private PlayerService _playerService;
    
        private float _verticalRotation;
        private const float _verticalBoards = 89f;
    
        [Inject]
        private void Inject(PlayerService playerService)
        {
            _playerService = playerService;
        }
    
        public override void Init()
        {
            _playerService.Ticked.Subscribe(Tick);
            Cursor.lockState = CursorLockMode.Locked;
        
            Controller.Interacted.Subscribe(OnInteracted);
        }
        
        private void Tick()
        {
            Vector3 moveDirection = new Vector3(Controller.MoveDirection.x, 0, Controller.MoveDirection.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _playerService.Config.MoveSpeed * Time.deltaTime;
        
            transform.position += moveDirection;
        
            float rotHorizontal = Controller.LookDirection.x * _playerService.Config.RotateSpeed;
            transform.Rotate(0, rotHorizontal, 0);
        
            _verticalRotation -= Controller.LookDirection.y * _playerService.Config.RotateSpeed;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalBoards, _verticalBoards);
            _camera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        }
    
        private void OnInteracted()
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                    out RaycastHit hit, _playerService.Config.InteractionDistance))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    interactable?.Interact();
                }
            }
        }
    }
}