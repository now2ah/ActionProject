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

        PlayerUnitData _playerUnitData;

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
        public PlayerUnitData PlayerUnitData { get { return _playerUnitData; } set { _playerUnitData = value; } }

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
            PlayerUnitData.exp += exp;
            //Logger.Log("Exp : " + _exp);
            while (_CanLevelUp())
                ModifyLevel(PlayerUnitData.level + 1);
        }

        public virtual void ModifyLevel(int level)
        {
            PlayerUnitData.level = level;
            PlayerUnitData.exp = PlayerUnitData.exp - PlayerUnitData.nextExp;
            float nextExp = PlayerUnitData.nextExp * 1.5f;
            PlayerUnitData.nextExp = (int)nextExp;
            _ApplyStats();
        }

        protected bool _CanLevelUp()
        {
            if (PlayerUnitData.nextExp <= PlayerUnitData.exp)
                return true;
            else
                return false;
        }

        protected void _ApplyStats()
        {
            UnitData.maxHp = UnitData.maxHp + (UnitData.growthHp * PlayerUnitData.level);
            UnitData.hp += UnitData.growthHp;
            PlayerUnitData.attackDamage = PlayerUnitData.attackDamage + (PlayerUnitData.growthAttackDamage * PlayerUnitData.level);
        }

        void _SetDefaultValue()
        {
            _playerUnitData.speed = 1.0f;
            _playerUnitData.attackDamage = 1.0f;
            _playerUnitData.level = 1;
            _playerUnitData.exp = 0;
            _playerUnitData.nextExp = 1;
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            _playerUnitData = new PlayerUnitData();
            _SetDefaultValue();
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