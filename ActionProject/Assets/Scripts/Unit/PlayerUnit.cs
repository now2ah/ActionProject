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

        GameObject _interactingBuilding;
        public GameObject InteractingBuilding { get { return _interactingBuilding; } set { _interactingBuilding = value; } }

        void OnMove(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_moveState);
            inputVector = context.ReadValue<Vector2>();
        }

        void OnMoveCanceled(InputAction.CallbackContext context)
        {
            base.StateMachine.ChangeState(_idleState);
            inputVector = Vector3.zero;
        }

        void OnActionPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                bool isPressed = context.ReadValueAsButton();
                Interact();
                Logger.Log(isPressed.ToString());
            }
        }

        void _CheckUnableInteractBuilding()
        {
            if (null == _interactingBuilding)
                return;
            else
            {
                float dist = Vector3.Distance(transform.position, _interactingBuilding.transform.position);
                if (_interactingBuilding.GetComponent<Building>().ActiveDistance < dist)
                    _interactingBuilding = null;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            name = "Commander";
            MaxHp = 200;
            HP = MaxHp - 50;
            SetNameUI(name);
        }

        public void Interact()
        {
            if(null != _interactingBuilding)
                _interactingBuilding.GetComponent<Building>().Interact();
        }

        public void Move()
        {
            Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);

            if (base.StateMachine.IsState(_moveState))
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * 25.0f, Space.World);
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
            _isMoving = false;
            InputManager.Instance.actionMove.performed += ctx => { OnMove(ctx); };
            InputManager.Instance.actionMove.canceled += ctx => { OnMoveCanceled(ctx); };
            InputManager.Instance.actionAction.performed += ctx => { OnActionPressed(ctx); };
            _idleState = new PlayerIdleState(this);
            _moveState = new PlayerMoveState(this);
            _animator = GetComponentInChildren<Animator>();
            _interactingBuilding = null;

            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            _CheckUnableInteractBuilding();
        }
    }

}