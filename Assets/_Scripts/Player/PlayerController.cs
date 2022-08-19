using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement), typeof(PlayerAttack))]
    public class PlayerController : MonoBehaviour
    {
        private Controls _controls;
        private PlayerInput _playerInput;
        private Camera _mainCam;
        
        [SerializeField] private DirectionInfo directionInfo;
        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;
        
        void Awake()
        {
            _controls = new Controls();
            _controls.Enable();
            _mainCam = Camera.main;
            _playerInput = GetComponent<PlayerInput>();
            if(!directionInfo)
                directionInfo = GetComponent<DirectionInfo>();
            if (!directionInfo)
                directionInfo = gameObject.AddComponent<DirectionInfo>();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerAttack = GetComponent<PlayerAttack>();

        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            var moveDir = ctx.ReadValue<Vector2>();
            // Debug.Log(moveDir);
            _playerMovement.Move(moveDir);
            directionInfo.moveDirection = moveDir;
        }

        public void OnLook(InputAction.CallbackContext ctx)
        {
            Vector2 lookInput;
            if (_playerInput.currentControlScheme == "Keyboard & Mouse")
            {
                Vector2 mousePos = _mainCam.ScreenToViewportPoint(Mouse.current.position.ReadValue());
                lookInput = (mousePos - (Vector2.one * .5f)).normalized;
            }
            else
                lookInput = ctx.ReadValue<Vector2>();

            directionInfo.lookDirection = lookInput;
        }


        public void OnUseAttack(InputAction.CallbackContext ctx)
        {
            // _playerAttack.AttackInput = ctx.ReadValue<float>() > 0;
            // _playerAttack.DirectionInfo = directionInfo;
        }

        private void Update()
        {
            _playerAttack.AttackInput = _controls.Gameplay.UseAttack.ReadValue<float>() > 0;
            _playerAttack.DirectionInfo = directionInfo;
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

            _controls.Gameplay.Movement.performed += OnMove;
            _controls.Gameplay.Look.performed += OnLook;
            _controls.Gameplay.UseAttack.performed += OnUseAttack;
        }

        private void OnDisable()
        {
            _controls.Disable();
            
            _controls.Gameplay.Movement.performed -= OnMove;
            _controls.Gameplay.Look.performed -= OnLook;
            _controls.Gameplay.UseAttack.performed -= OnUseAttack;

        }
    }
}
