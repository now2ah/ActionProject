using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;

namespace Action.Units
{
    public class RangeEnemy : EnemyUnit
    {
        float _attackSpeed;
        float _attackDistance;
        float _lastAttackTime;

        RangeEnemyIdleState _idleState;
        RangeEnemyMoveState _moveState;
        RangeEnemyAttackState _attackState;

        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }

        public RangeEnemyIdleState IdleState => _idleState;
        public RangeEnemyMoveState MoveState => _moveState;
        public RangeEnemyAttackState AttackState => _attackState;

        public void Attack(int damage)
        {
            if (null != _target)
            {
                _CreateProjectile();
                _lastAttackTime = Time.realtimeSinceStartup;
            }
            //Melee
            //if (null != _target)
            //{
            //    if (_target.TryGetComponent<Unit>(out Unit unit))
            //    {
            //        Logger.Log("Attack");
            //        DamageMessage msg = new DamageMessage
            //        {
            //            damager = this,
            //            amount = damage
            //        };
            //        unit.ApplyDamage(msg);
            //        _lastAttackTime = Time.realtimeSinceStartup;
            //    }
            //}
        }

        public bool isAttackCooltime()
        {
            if (Time.realtimeSinceStartup < _lastAttackTime + _attackSpeed)
                return true;
            else
                return false;
        }

        public bool isTargetInDistance()
        {
            float dist = Vector3.Distance(transform.position, _target.transform.position);
            Logger.Log(dist.ToString());
            if (dist < _attackDistance)
                return true;
            else
                return false;
        }

        public override void RefreshTargetPosition()
        {
            if (_attackState != StateMachine.CurState)
                SetDestinationToTarget(_target);
        }

        void _CreateProjectile()
        {
            if (null != _target)
            {
                Vector3 shootPosition = transform.position + transform.forward * 2.0f + transform.up * 2.0f;
                Instantiate(GameManager.Instance.ProjectilePrefab, shootPosition, transform.rotation);
            }
        }

        protected override void Start()
        {
            base.Start();
            _attackSpeed = 1.0f;
            _attackDistance = 10.0f;
            _lastAttackTime = 0.0f;
            _idleState = new RangeEnemyIdleState(this);
            _moveState = new RangeEnemyMoveState(this);
            _attackState = new RangeEnemyAttackState(this);
            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            Debug.DrawRay(transform.position, transform.forward, Color.red);
        }
    }
}

