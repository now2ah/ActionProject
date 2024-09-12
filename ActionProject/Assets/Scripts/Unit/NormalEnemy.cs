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

        EnemyIdleState _idleState;
        EnemyMoveState _moveState;
        EnemyAttackState _attackState;

        int _animHashMoving;
        int _animHashAttacking;

        public Animator Animator => _animator;
        public bool IsAttackCooltime => _isAttackCooltime;
        public ActionTime AttackTimer => _attackTimer;
        public EnemyIdleState IdleState => _idleState;
        public EnemyMoveState MoveState => _moveState;
        public EnemyAttackState AttackState => _attackState;
        public int AnimHashMoving => _animHashMoving;
        public int AnimHashAttacking => _animHashAttacking;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            _animHashMoving = Animator.StringToHash("IsMoving");
            _animHashAttacking = Animator.StringToHash("IsAttacking");

            SetNameUI(UnitName);
            StateMachine.Initialize(_idleState);
        }

        public override void RefreshTargetPosition()
        {
            base.RefreshTargetPosition();

            if (!gameObject.activeSelf)
                return;

            FindNearestTarget(true);
            if (_moveState == StateMachine.CurState)
                SetDestinationToTarget(_target);
        }

        public void Stop(float stopTime, UnityAction action = null)
        {
            StopCoroutine("StopMoveCoroutine");
            StartCoroutine(StopMoveCoroutine(stopTime, action));
        }

        IEnumerator StopMoveCoroutine(float stopTime, UnityAction action = null)
        {
            _ResetTarget();
            yield return new WaitForSeconds(stopTime);
            if (null != action)
                action.Invoke();
        }

        private void OnCollisionStay(Collision col)
        {
            if (!_isAttackCooltime && "PlayerObject" == col.gameObject.tag)
            {
                Unit colUnit = col.gameObject.GetComponent<Unit>();
                DamageMessage msg = new DamageMessage
                {
                    damager = this,
                    amount = EnemyUnitData.attackDamage
                };
                colUnit.ApplyDamage(msg);
                _isAttackCooltime = true;
                _attackTimer.TickStart(EnemyUnitData.attackSpeed);
            }
        }

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
            EnemyUnitData.speed = _unitStats.speed;
            EnemyUnitData.attackDamage = _unitStats.attackDamage;
            EnemyUnitData.attackSpeed = _unitStats.attackSpeed;
            EnemyUnitData.attackDistance = _unitStats.attackDistance;
            EnemyUnitData.expAmount = _unitStats.expAmount;
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
        }
    }
}

