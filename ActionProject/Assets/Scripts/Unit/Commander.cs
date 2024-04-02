using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.State;
using Action.Manager;

namespace Action.Units
{
    public class Commander : PlayerUnit
    {
        Vector2 inputVector;

        CommanderIdleState _idleState;
        CommanderMoveState _moveState;
        CommanderAttackState _attackState;

        GameObject _interactingBuilding;

        IEnumerator _physicalAttackCoroutine;

        public CommanderIdleState IdleState => _idleState;
        public CommanderMoveState MoveState => _moveState;
        public CommanderAttackState AttackState => _attackState;
        public GameObject InteractingBuilding { get { return _interactingBuilding; } set { _interactingBuilding = value; } }

        public override void Initialize()
        {
            base.Initialize();
            name = "Commander";
            MaxHp = 200;
            HP = MaxHp - 50;
            SetNameUI(name);
            UnitPanel.Show();
        }

        public void Interact()
        {
            if (null != _interactingBuilding)
                _interactingBuilding.GetComponent<Building>().Interact();
        }

        public void Move()
        {
            Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);

            if (StateMachine.IsState(_moveState))
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

            transform.Translate(movePos * Time.deltaTime * 10.0f, Space.World);
        }

        public void PhysicalAttack()
        {
            StopCoroutine(PhysicalAttackCoroutine());
            StartCoroutine(PhysicalAttackCoroutine());
        }

        void OnMove(InputAction.CallbackContext context)
        {
            StateMachine.ChangeState(_moveState);
            inputVector = context.ReadValue<Vector2>();
        }

        void OnMoveCanceled(InputAction.CallbackContext context)
        {
            StateMachine.ChangeState(_idleState);
            inputVector = Vector3.zero;
        }

        void OnActionPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                Interact();
        }

        void OnPhysicalAttackPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
                StateMachine.ChangeState(_attackState);
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

        IEnumerator PhysicalAttackCoroutine()
        {
            _isAttacking = true;
            _animator.SetBool("isAttacking", _isAttacking);
            yield return new WaitForSeconds(1.0f);
            _isAttacking = false;
            _animator.SetBool("isAttacking", _isAttacking);
        }

        protected override void Awake()
        {
            base.Awake();
            _isMoving = false;
            _interactingBuilding = null;
            _animator = GetComponentInChildren<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
            InputManager.Instance.actionMove.performed += ctx => { OnMove(ctx); };
            InputManager.Instance.actionMove.canceled += ctx => { OnMoveCanceled(ctx); };
            InputManager.Instance.actionAction.performed += ctx => { OnActionPressed(ctx); };
            InputManager.Instance.actionPhysicalAttack.performed += ctx => { OnPhysicalAttackPressed(ctx); };
            _idleState = new CommanderIdleState(this);
            _moveState = new CommanderMoveState(this);
            _attackState = new CommanderAttackState(this);
            _physicalAttackCoroutine = PhysicalAttackCoroutine();
            StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            _CheckUnableInteractBuilding();
        }
    }
}
