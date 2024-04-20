using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Util;

namespace Action.Units
{
    public class NormalEnemy : EnemyUnit
    {
        bool _isAttackCooltime;
        ActionTime _attackTimer;

        float _attackCooltime = 1.0f;

        EnemyIdleState _idleState;
        EnemyMoveState _moveState;
        EnemyAttackState _attackState;

        public EnemyIdleState IdleState => _idleState;
        public EnemyMoveState MoveState => _moveState;
        public EnemyAttackState AttackState => _attackState;

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
                _attackTimer.TickStart(_attackCooltime);
            }
        }

        void _CheckAttackCoolTime()
        {
            if (null != _attackTimer)
            {
                if (_attackTimer.IsFinish)
                    _isAttackCooltime = false;
            }
        }

        protected override void Start()
        {
            base.Start();
            _isAttackCooltime = false;
            _attackTimer = gameObject.AddComponent<ActionTime>();
            _idleState = new EnemyIdleState(this);
            _moveState = new EnemyMoveState(this);
            _attackState = new EnemyAttackState(this);
            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            _CheckAttackCoolTime();
        }
    }
}

