using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public event EventHandler<float> OnWheelAction;
        public event EventHandler<Vector2> OnMouseLookAction;
        public event EventHandler<bool> OnAttackActionDown;
        public event EventHandler<bool> OnAttackAction;
        public event EventHandler<bool> OnAttackActionUp;

        InputAction moveAction;
        InputAction wheelAction;
        InputAction mouseLookAction;
        InputAction attackAction;

        private void OnEnable()
        {
            moveAction = IAAsset.FindAction("Move");
            wheelAction = IAAsset.FindAction("Wheel");
            mouseLookAction = IAAsset.FindAction("Look");
            attackAction = IAAsset.FindAction("Attack");

            moveAction.started += OnMoveActionStarted;
            moveAction.performed += OnMoveActionPerformed;
            moveAction.canceled += OnMoveActionCanceled;
            wheelAction.performed += OnWheelActionPerformed;
            mouseLookAction.performed += OnMouseLookActionPerformed;
            attackAction.started += OnAttackActionStarted;
            attackAction.performed += OnAttackActionPerformed;
            attackAction.canceled += OnAttackActionCanceled;



            moveAction.Enable();
            wheelAction.Enable();
            mouseLookAction.Enable();
            attackAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.started -= OnMoveActionStarted;
            moveAction.performed -= OnMoveActionPerformed;
            moveAction.canceled -= OnMoveActionCanceled;
            wheelAction.performed -= OnWheelActionPerformed;
            mouseLookAction.performed -= OnMouseLookActionPerformed;
            attackAction.started -= OnAttackActionStarted;
            attackAction.performed -= OnAttackActionPerformed;
            attackAction.canceled -= OnAttackActionCanceled;

            moveAction.Disable();
            wheelAction.Disable();
            mouseLookAction.Disable();
            attackAction.Disable();
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

        void OnWheelActionPerformed(InputAction.CallbackContext context)
        {
            OnWheelAction.Invoke(this, context.ReadValue<float>());
        }

        void OnMouseLookActionPerformed(InputAction.CallbackContext context)
        {
            OnMouseLookAction.Invoke(this, context.ReadValue<Vector2>());
        }

        void OnAttackActionStarted(InputAction.CallbackContext context)
        {
            OnAttackActionDown.Invoke(this, context.ReadValueAsButton());
        }

        void OnAttackActionPerformed(InputAction.CallbackContext context)
        {
            OnAttackAction.Invoke(this, context.ReadValueAsButton());
        }

        void OnAttackActionCanceled(InputAction.CallbackContext context)
        {
            OnAttackActionUp.Invoke(this, context.ReadValueAsButton());
        }
    }
}

