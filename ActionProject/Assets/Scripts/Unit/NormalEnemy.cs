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
        UnitStatsSO _unitStats;
        bool _isAttackCooltime;
        ActionTime _attackTimer;

        EnemyIdleState _idleState;
        EnemyMoveState _moveState;
        EnemyAttackState _attackState;

        public bool IsAttackCooltime => _isAttackCooltime;
        public ActionTime AttackTimer => _attackTimer;
        public EnemyIdleState IdleState => _idleState;
        public EnemyMoveState MoveState => _moveState;
        public EnemyAttackState AttackState => _attackState;

        public override void Initialize()
        {
            base.Initialize();
            UnitName = _unitStats.unitName;
            MaxHp = _unitStats.maxHp;
            HP = _unitStats.maxHp;
            AttackDamage = _unitStats.attackDamage;
            AttackSpeed = _unitStats.attackSpeed;
            SetNameUI(UnitName);
        }

        public override void RefreshTargetPosition()
        {
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
                    amount = AttackDamage
                };
                colUnit.ApplyDamage(msg);
                _isAttackCooltime = true;
                _attackTimer.TickStart(AttackSpeed);
            }
        }

        void _CheckAttackCoolTime()
        {
            if (null != _attackTimer)
            {
                if (_attackTimer.IsFinish)
                {
                    _isAttackCooltime = false;
                    _attackTimer.ResetTimer();
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
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
            StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            _CheckAttackCoolTime();
        }
    }
}

