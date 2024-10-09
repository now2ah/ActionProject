using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.State;
using Action.Util;
using Action.SO;

namespace Action.Units
{
    public class NormalEnemy : EnemyUnit
    {
        Animator _animator;

        UnitStatsSO _unitStats;
        bool _isAttackCooltime;
        ActionTime _attackTimer;

        float _lastAttackTime;

        EnemyIdleState _idleState;
        EnemyMoveState _moveState;
        EnemyAttackState _attackState;

        int _animHashMoving;
        int _animHashAttacking;
        int _animHashSpeed;

        public Animator Animator => _animator;
        public bool IsAttackCooltime => _isAttackCooltime;
        public ActionTime AttackTimer => _attackTimer;
        public EnemyIdleState IdleState => _idleState;
        public EnemyMoveState MoveState => _moveState;
        public EnemyAttackState AttackState => _attackState;
        public int AnimHashMoving => _animHashMoving;
        public int AnimHashAttacking => _animHashAttacking;
        public int AnimHashSpeed => _animHashSpeed;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            _animHashMoving = Animator.StringToHash("IsMoving");
            _animHashAttacking = Animator.StringToHash("IsAttacking");
            _animHashSpeed = Animator.StringToHash("Speed");

            SetNameUI(UnitData.name);
            SetSpeed(((EnemyUnitData)UnitData).speed);
            StateMachine.Initialize(_idleState);
        }

        public override void RefreshTargetPosition()
        {
            base.RefreshTargetPosition();

            if (!gameObject.activeSelf)
                return;

            FindNearestPlayerBuilding();
            FindNearestTarget(true);
            if (_moveState == StateMachine.CurState)
                SetDestinationToTarget(_target);
        }

        public void Attack()
        {
            if (null != _target)
            {
                _lastAttackTime = Time.realtimeSinceStartup;
                _DeliverDamage();
            }
        }

        void _DeliverDamage()
        {
            DamageMessage msg = new DamageMessage
            {
                damager = this,
                amount = ((EnemyUnitData)UnitData).attackDamage
            };

            if (_target.TryGetComponent<Unit>(out Unit comp))
            {
                comp.ApplyDamage(msg);
                _isAttackCooltime = true;
                _attackTimer.TickStart(((EnemyUnitData)UnitData).attackSpeed);
            }
        }

        public bool isAttackCooltime()
        {
            if (Time.realtimeSinceStartup < _lastAttackTime + ((EnemyUnitData)UnitData).attackSpeed)
                return true;
            else
                return false;
        }

        public bool isTargetInDistance()
        {
            float dist = Vector3.Distance(transform.position, Target.transform.position);
            if (dist < ((EnemyUnitData)UnitData).attackDistance)
                return true;
            else
                return false;
        }

        //private void OnCollisionStay(Collision col)
        //{
        //    if (!_isAttackCooltime && "PlayerObject" == col.gameObject.tag)
        //    {
        //        Unit colUnit = col.gameObject.GetComponent<Unit>();
        //        DamageMessage msg = new DamageMessage
        //        {
        //            damager = this,
        //            amount = EnemyUnitData.attackDamage
        //        };
        //        colUnit.ApplyDamage(msg);
        //        _isAttackCooltime = true;
        //        _attackTimer.TickStart(EnemyUnitData.attackSpeed);
        //    }
        //}

        void _CheckAttackCoolTime()
        {
            if (null != _attackTimer)
            {
                if (_attackTimer.IsFinished)
                {
                    _isAttackCooltime = false;
                    _attackTimer.ResetTimer();
                }
            }
        }

        void _SetUnitData()
        {
            UnitData.name = _unitStats.unitName;
            UnitData.hp = _unitStats.maxHp;
            UnitData.maxHp = _unitStats.maxHp;
            UnitData.growthHp = _unitStats.growthMaxHp;
            ((EnemyUnitData)UnitData).speed = _unitStats.speed;
            ((EnemyUnitData)UnitData).attackDamage = _unitStats.attackDamage;
            ((EnemyUnitData)UnitData).attackSpeed = _unitStats.attackSpeed;
            ((EnemyUnitData)UnitData).attackDistance = _unitStats.attackDistance;
            ((EnemyUnitData)UnitData).expAmount = _unitStats.expAmount;
            ((EnemyUnitData)UnitData).goldAmount = _unitStats.goldAmount;
        }

        void OnAnimatorMove()
        {
            Vector3 position = _animator.rootPosition;
            position.y = NavMeshAgentComp.nextPosition.y;
            transform.position = position;
            NavMeshAgentComp.nextPosition = transform.position;
        }

        protected override void Awake()
        {
            base.Awake();
            _animator = GetComponentInChildren<Animator>();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/NormalEnemyStats") as UnitStatsSO;
            _isAttackCooltime = false;
            _attackTimer = gameObject.AddComponent<ActionTime>();
            _idleState = new EnemyIdleState(this);
            _moveState = new EnemyMoveState(this);
            _attackState = new EnemyAttackState(this);
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
            
        }

        protected override void Update()
        {
            base.Update();
            _CheckAttackCoolTime();
            _animator.SetFloat(_animHashSpeed, NavMeshAgentComp.speed);
        }
    }
}

