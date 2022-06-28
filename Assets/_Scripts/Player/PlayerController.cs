using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(Directed))]
    public class PlayerController : MonoBehaviour
    {
        private Controls _controls;
        private PlayerInput _playerInput;
        private Camera _mainCam;
        
        private Directed _directed;
        private PlayerMovement _playerMovement;
        // private PlayerAttack _playerAttack;
        
        void Awake()
        {
            _controls = new Controls();
            _controls.Enable();
            _mainCam = Camera.main;
            _playerInput = GetComponent<PlayerInput>();
            _directed = GetComponent<Directed>();
            _playerMovement = GetComponent<PlayerMovement>();

        }

        public void Move(InputAction.CallbackContext ctx)
        {
            var moveDir = ctx.ReadValue<Vector2>();
            Debug.Log(moveDir);
            _playerMovement.Move(moveDir);
            _directed.moveDirection = moveDir;
        }

        public void Look(InputAction.CallbackContext ctx)
        {
            Vector2 lookInput;
            if (_playerInput.currentControlScheme == "Keyboard & Mouse")
            {
                Vector2 mousePos = _mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
                lookInput = (mousePos - (Vector2.one * .5f)).normalized;
            }
            else
                lookInput = ctx.ReadValue<Vector2>();

            _directed.lookDirection = lookInput;
        }
        
        // public void Activate(InputAction.CallbackContext ctx)
        // {
        //     ActivateInput = ctx.performed;
        // }


        // private void Update()
        // {
        //     if (_playerInput.currentControlScheme == "Keyboard & Mouse")
        //     {
        //         Vector2 mousePos = mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
        //         LookInput = (mousePos - (Vector2.one * .5f)).normalized;
        //     }
        // }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }
    }
}
