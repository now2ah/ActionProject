using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.State;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class PlayerUnit : Unit
    {
        bool _isMoving = false;
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }

        Vector2 inputVector;

        PlayerIdleState _idleState;
        PlayerMoveState _moveState;

        Animator _animator;
        public Animator Animator => _animator;

        void OnTestAction(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_moveState);
            inputVector = context.ReadValue<Vector2>();
        }

        void OnTestActionCanceled(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_idleState);
            inputVector = Vector3.zero;
        }

        void OnActionCallback(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                bool isPressed = context.ReadValueAsButton();
                Logger.Log(isPressed.ToString());
            }
        }

        public void Move()
        {
            Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);

            if (base.StateMachine.IsState(_moveState))
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * 5.0f, Space.World);
        }

        protected override void Start()
        {
            FullHp = 200;
            HP = FullHp - 50;

            base.Start();

            _isMoving = false;
            InputManager.Instance.actionMove.performed += ctx => { OnTestAction(ctx); };
            InputManager.Instance.actionMove.canceled += ctx => { OnTestActionCanceled(ctx); };
            InputManager.Instance.actionAction.performed += ctx => { OnActionCallback(ctx); };
            _idleState = new PlayerIdleState(this);
            _moveState = new PlayerMoveState(this);
            _animator = GetComponentInChildren<Animator>();
            
            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}