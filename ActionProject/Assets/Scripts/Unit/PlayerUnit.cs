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
        protected bool _isMoving = false;
        protected bool _isAttacking = false;

        PlayerIdleState _idleState;
        PlayerMoveState _moveState;

        protected Animator _animator;
        
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
        public Animator Animator => _animator;
        public override void Initialize()
        {
            base.Initialize();
        }

        public virtual void Move()
        {

        }

        protected override void Start()
        {
            base.Start();
            Initialize();
            _isMoving = false;
            _idleState = new PlayerIdleState(this);
            _moveState = new PlayerMoveState(this);
            _animator = GetComponentInChildren<Animator>();

            StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}