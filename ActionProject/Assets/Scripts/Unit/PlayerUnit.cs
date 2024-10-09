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

        public bool IsMoving { get { return _isMoving; } set { _isMoving = value; } }
        public bool IsAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
        public Animator Animator => _animator;
        public NavMeshAgent NavMeshAgentComp { get { return _navMeshAgent; } set { _navMeshAgent = value; } }

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
            if (UnitData is PlayerUnitData)
            {
                ((PlayerUnitData)UnitData).exp += exp;
                while (_CanLevelUp())
                    ModifyLevel(((PlayerUnitData)UnitData).level + 1);
            }
        }

        public virtual void ModifyLevel(int level)
        {
            if (UnitData is PlayerUnitData)
            {
                ((PlayerUnitData)UnitData).level = level;
                ((PlayerUnitData)UnitData).exp = ((PlayerUnitData)UnitData).exp - ((PlayerUnitData)UnitData).nextExp;
                float nextExp = ((PlayerUnitData)UnitData).nextExp * 1.5f;
                ((PlayerUnitData)UnitData).nextExp = (int)nextExp;
                _ApplyStats();
            }
        }

        protected bool _CanLevelUp()
        {
            if (UnitData is PlayerUnitData)
            {
                if (((PlayerUnitData)UnitData).nextExp <= ((PlayerUnitData)UnitData).exp)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        protected void _ApplyStats()
        {
            if (UnitData is PlayerUnitData)
            {
                UnitData.maxHp = UnitData.maxHp + (UnitData.growthHp * ((PlayerUnitData)UnitData).level);
                UnitData.hp += UnitData.growthHp;
                ((PlayerUnitData)UnitData).attackDamage = ((PlayerUnitData)UnitData).attackDamage + (((PlayerUnitData)UnitData).growthAttackDamage * ((PlayerUnitData)UnitData).level);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            UnitData = new PlayerUnitData();
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