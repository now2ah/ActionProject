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
        Animator _animator;

        UnitStatsSO _unitStats;

        float _lastAttackTime;

        RangeEnemyIdleState _idleState;
        RangeEnemyMoveState _moveState;
        RangeEnemyAttackState _attackState;

        int _animHashMoving;
        int _animHashAttacking;
        int _animHashSpeed;

        public Animator Animator => _animator;
        public RangeEnemyIdleState IdleState => _idleState;
        public RangeEnemyMoveState MoveState => _moveState;
        public RangeEnemyAttackState AttackState => _attackState;
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

        public void Attack(float damage)
        {
            if (null != _target)
            {
                _CreateProjectile(damage);
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
            if (Time.realtimeSinceStartup < _lastAttackTime + ((EnemyUnitData)UnitData).attackSpeed)
                return true;
            else
                return false;
        }

        public bool isTargetInDistance()
        {
            float dist = Vector3.Distance(transform.position, _target.transform.position);
            if (dist < ((EnemyUnitData)UnitData).attackDistance)
                return true;
            else
                return false;
        }

        public override void RefreshTargetPosition()
        {
            base.RefreshTargetPosition();

            if (!gameObject.activeSelf)
                return;

            FindNearestPlayerBuilding();
            FindNearestTarget(true);
            //if (_attackState != StateMachine.CurState)
            //    SetDestinationToTarget(_target);
        }

        void _CreateProjectile(float damage)
        {
            if (null != _target)
            {
                Vector3 shootPosition = transform.position + transform.forward * 2.0f + transform.up * 1.2f;
                //shootPosition.y = GameManager.Instance.Constants.HUNT_PROJECTILE_Y_POS;
                RangeEnemyProjectile projectile = (RangeEnemyProjectile)PoolManager.Instance.RangeEnemyProjectilePool.GetNew();
                projectile.transform.position = shootPosition;
                projectile.transform.rotation = transform.rotation;
                projectile.Initialize(this, damage);
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
            _unitStats = Resources.Load("ScriptableObject/UnitStats/RangeEnemyStats") as UnitStatsSO;
            _lastAttackTime = 0.0f;
            _idleState = new RangeEnemyIdleState(this);
            _moveState = new RangeEnemyMoveState(this);
            _attackState = new RangeEnemyAttackState(this);
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected void FixedUpdate()
        {
            _animator.SetFloat(_animHashSpeed, NavMeshAgentComp.speed);
        }
    }
}

