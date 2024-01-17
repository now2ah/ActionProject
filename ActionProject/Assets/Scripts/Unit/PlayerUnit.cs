using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.State;
using Action.Manager;

namespace Action.Units
{
    public class PlayerUnit : Unit
    {
        bool _isMoving = false;
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }

        Vector2 inputVector;

        PlayerIdleState _idleState;
        PlayerMovingState _movingState;

        Animator _animator;
        public Animator Animator => _animator;

        void OnTestAction(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_movingState);
            inputVector = context.ReadValue<Vector2>();
        }

        void OnTestActionCanceled(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_idleState);
            inputVector = Vector3.zero;
        }

        public void Move()
        {
            Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);

            if (base.StateMachine.IsState(_movingState))
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * 5.0f, Space.World);
        }

        public override void Start()
        {
            base.Start();
            _isMoving = false;
            InputManager.Instance.actionMove.performed += ctx => { OnTestAction(ctx); };
            InputManager.Instance.actionMove.canceled += ctx => { OnTestActionCanceled(ctx); };
            _idleState = new PlayerIdleState(this);
            _movingState = new PlayerMovingState(this);
            _animator = GetComponentInChildren<Animator>();

            base.StateMachine.Initialize(_idleState);
        }

        public override void Update()
        {
            base.Update();
        }
    }

}