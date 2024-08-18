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

        [SerializeReference]
        float _speed;

        [SerializeReference]
        float _attackDamage;
        float _growthAttackDamage;

        int _level;
        int _exp;
        int _nextExp;

        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
        public Animator Animator => _animator;
        public NavMeshAgent NavMeshAgentComp { get { return _navMeshAgent; } set { _navMeshAgent = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public float GrowthAttackDamage { get { return _growthAttackDamage; } set { _growthAttackDamage = value; } }
        public int Level { get { return _level; } set { _level = value; } }
        public int Exp { get { return _exp; } set { _exp = value; } }
        public int NextExp { get { return _nextExp; } set { _nextExp = value; } }

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

        public virtual void GainExp(int exp)
        {
            _exp += exp;
            //Logger.Log("Exp : " + _exp);
            if (_CanLevelUp())
                ModifyLevel(_level + 1);
        }

        public virtual void ModifyLevel(int level)
        {
            _level = level;
            _exp = 0;
            float nextExp = _nextExp * 1.5f;
            _nextExp = (int)nextExp;
            _ApplyStats();
        }

        protected bool _CanLevelUp()
        {
            if (_nextExp <= _exp)
                return true;
            else
                return false;
        }

        protected void _ApplyStats()
        {
            MaxHp = MaxHp + (GrowthHp * _level);
            HP += GrowthHp;
            AttackDamage = AttackDamage + (GrowthAttackDamage * _level);
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            _speed = 1.0f;
            _attackDamage = 1.0f;
            _level = 1;
            _exp = 0;
            _nextExp = 1;
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
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