using Content.Scripts.Controllers;
using Content.Scripts.Services;
using UnityEngine;
using VContainer;

namespace Content.Scripts.Controllers
{
    public class PlayerController : Controller
    {
        [SerializeField] private Camera _camera;
        
        private PlayerService _playerService;
        private InputService _inputService;
        
        private float _verticalRotation;
        
        [Inject]
        private void Inject(PlayerService playerService, InputService inputService)
        {
            _playerService = playerService;
            _inputService = inputService;
        }

        public override void Init()
        {
            _playerService.Ticked.Subscribe(Tick);
            Cursor.lockState = CursorLockMode.Locked;

            _inputService.Interacted.Subscribe(OnInteracted);
        }

        private void Tick()
        {
            Vector3 moveDirection = new Vector3(_inputService.MoveInput.X, 0, _inputService.MoveInput.Y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _playerService.Config.MoveSpeed * Time.deltaTime;

            transform.position += moveDirection;
            
            float rotHorizontal = _inputService.LookInput.X * _playerService.Config.RotateSpeed;
            transform.Rotate(0, rotHorizontal, 0);

            _verticalRotation -= _inputService.LookInput.Y * _playerService.Config.RotateSpeed;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -89, 89);
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