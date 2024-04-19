using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class MeleeEnemy : EnemyUnit
    {
        float _attackSpeed;
        float _attackDistance;
        float _lastAttackTime;

        MeleeEnemyIdleState _idleState;
        MeleeEnemyMoveState _moveState;
        MeleeEnemyAttackState _attackState;

        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }

        public MeleeEnemyIdleState IdleState => _idleState;
        public MeleeEnemyMoveState MoveState => _moveState;
        public MeleeEnemyAttackState AttackState => _attackState;

        public void Attack(int damage)
        {
            if (null != _target)
            {
                if (_target.TryGetComponent<Unit>(out Unit unit))
                {
                    Logger.Log("Attack");
                    DamageMessage msg = new DamageMessage
                    {
                        damager = this,
                        amount = damage
                    };
                    unit.ApplyDamage(msg);
                    _lastAttackTime = Time.realtimeSinceStartup;
                }
            }
        }

        public bool isAttackCooltime()
        {
            if (Time.realtimeSinceStartup < _lastAttackTime + _attackSpeed)
                return true;
            else
                return false;
        }

        protected override void Start()
        {
            base.Start();
            _attackSpeed = 1.0f;
            _attackDistance = 1.0f;
            _lastAttackTime = 0.0f;
            _idleState = new MeleeEnemyIdleState(this);
            _moveState = new MeleeEnemyMoveState(this);
            _attackState = new MeleeEnemyAttackState(this);
            base.StateMachine.Initialize(_idleState);
        }
    }
}

