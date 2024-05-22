using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;
using Action.SO;
using Action.Game;

namespace Action.Units
{
    public class RangeEnemy : EnemyUnit
    {
        UnitStatsSO _unitStats;

        float _lastAttackTime;

        ObjectPooler<Projectile> _projectilePool;

        RangeEnemyIdleState _idleState;
        RangeEnemyMoveState _moveState;
        RangeEnemyAttackState _attackState;

        public RangeEnemyIdleState IdleState => _idleState;
        public RangeEnemyMoveState MoveState => _moveState;
        public RangeEnemyAttackState AttackState => _attackState;

        public override void Initialize()
        {
            base.Initialize();
            UnitName = _unitStats.unitName;
            MaxHp = _unitStats.maxHp;
            HP = _unitStats.maxHp;
            AttackDamage = _unitStats.attackDamage;
            AttackSpeed = _unitStats.attackSpeed;
            AttackDistance = _unitStats.attackDistance;
            SetNameUI(UnitName);
            Projectile projectile = GameManager.Instance.ProjectilePrefab.GetComponent<Projectile>();
            _projectilePool.Initialize(projectile, 25);
        }

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
            if (Time.realtimeSinceStartup < _lastAttackTime + AttackSpeed)
                return true;
            else
                return false;
        }

        public bool isTargetInDistance()
        {
            float dist = Vector3.Distance(transform.position, _target.transform.position);
            //Logger.Log(dist.ToString());
            if (dist < AttackDistance)
                return true;
            else
                return false;
        }

        public override void RefreshTargetPosition()
        {
            FindNearestTarget(true);
            if (_attackState != StateMachine.CurState)
                SetDestinationToTarget(_target);
        }

        void _CreateProjectile()
        {
            if (null != _target)
            {
                Vector3 shootPosition = transform.position + transform.forward * 2.0f + transform.up * 2.0f;
                Game.Projectile projectile = _projectilePool.GetNew();
                projectile.transform.position = shootPosition;
                projectile.transform.rotation = transform.rotation;

                //Instantiate(GameManager.Instance.ProjectilePrefab, shootPosition, transform.rotation);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/RangeEnemyStats") as UnitStatsSO;
            _lastAttackTime = 0.0f;
            _projectilePool = new ObjectPooler<Projectile>();
            _idleState = new RangeEnemyIdleState(this);
            _moveState = new RangeEnemyMoveState(this);
            _attackState = new RangeEnemyAttackState(this);
        }

        protected override void Start()
        {
            base.Start();
            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

