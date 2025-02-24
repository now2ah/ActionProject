using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "ScriptableObject/InputManagerSO")]
    public class InputManagerSO : ScriptableObject
    {
        [SerializeField]
        public InputActionAsset IAAsset;

        public event EventHandler<Vector2> OnMoveActionDown;
        public event EventHandler<Vector2> OnMoveAction;
        public event EventHandler<Vector2> OnMoveActionUp;

        InputAction moveAction;

        private void OnEnable()
        {
            moveAction = IAAsset.FindAction("Move");

            moveAction.started += OnMoveActionStarted;
            moveAction.performed += OnMoveActionPerformed;
            moveAction.canceled += OnMoveActionCanceled;

            moveAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.started -= OnMoveActionStarted;
            moveAction.performed -= OnMoveActionPerformed;
            moveAction.canceled -= OnMoveActionCanceled;

            moveAction.Disable();
        }

        void OnMoveActionStarted(InputAction.CallbackContext context)
        {
            OnMoveActionDown.Invoke(this, context.ReadValue<Vector2>());
        }

        void OnMoveActionPerformed(InputAction.CallbackContext context)
        {
            OnMoveAction.Invoke(this, context.ReadValue<Vector2>());
        }

        void OnMoveActionCanceled(InputAction.CallbackContext context)
        {
            OnMoveActionUp.Invoke(this, context.ReadValue<Vector2>());
        }
    }
}

