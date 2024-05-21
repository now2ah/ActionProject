using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Action.State;
using Action.Manager;
using Action.UI;

namespace Action.Units
{
    public class PlayerUnit : Unit, IMovable
    {
        protected bool _isMoving = false;
        protected bool _isAttacking = false;

        PlayerIdleState _idleState;
        PlayerMoveState _moveState;

        protected Animator _animator;

        NavMeshAgent _navMeshAgent;
        float _speed;
        float _attackDamage;
        
        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
        public Animator Animator => _animator;
        public NavMeshAgent NavMeshAgentComp { get { return _navMeshAgent; } set { _navMeshAgent = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        
        public override void Initialize()
        {
            base.Initialize();
        }

        public void SetSpeed(float speed)
        {
            if (null != _navMeshAgent)
                _navMeshAgent.speed = speed;
        }

        public virtual void Move()
        {

        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
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