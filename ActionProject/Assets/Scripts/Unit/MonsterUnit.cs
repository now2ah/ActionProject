using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Action.State;
using Action.Util;
using Action.Manager;

namespace Action.Units
{
    public class MonsterUnit : Unit
    {
        NavMeshAgent _navMeshAgent;

        int _attackDamage;
        float _attackSpeed;
        float _attackDistance;
        float _lastAttackTime;
        MonsterIdleState _idleState;
        MonsterMoveState _moveState;
        MonsterAttackState _attackState;
        GameObject _target;
        GameObject _nearestPlayerBuilding;
        GameObject _commanderUnit;
        Vector3 _targetPos;

        public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
        public MonsterIdleState IdleState => _idleState;
        public MonsterMoveState MoveState => _moveState;
        public MonsterAttackState AttackState => _attackState;
        public GameObject Target { get { return _target; } set { _target = value; } }
        public Vector3 TargetPos { get { return _targetPos; } set { _targetPos = value; } }

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.OnRefresh.AddListener(_RefreshTargetPosition);
            IsOnUnitPanel = false;
        }

        public void FindNearestPlayerBuilding()
        {
            if (1 == GameManager.Instance.PlayerBuildings.Count)
            {
                _nearestPlayerBuilding = GameManager.Instance.PlayerBase;
                return;
            }

            float nearest = Mathf.Infinity;
            for (int i = 0 ; i < GameManager.Instance.PlayerBuildings.Count ; i++)
            {
                Vector3 dist = GameManager.Instance.PlayerBuildings[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    _nearestPlayerBuilding = GameManager.Instance.PlayerBuildings[i];
                }
            }
        }

        public void FindNearestTarget(bool isIncludeCmd)  /*Commander 포함 여부*/
        {
            float nearest = Mathf.Infinity;
            for (int i = 0 ; i < GameManager.Instance.PlayerUnits.Count ; i++)
            {
                if (!isIncludeCmd && GameManager.Instance.PlayerUnits[i] == GameManager.Instance.CommanderUnit)
                    continue;

                Vector3 dist = GameManager.Instance.PlayerUnits[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    _target = GameManager.Instance.PlayerUnits[i];
                }
            }

            //nearestBuilding이랑 거리 비교
            if (null != _nearestPlayerBuilding)
            {
                GameObject target = Utility.GetNearerObject(gameObject.transform.position, _target, _nearestPlayerBuilding);
                _target = target;
            }
        }

        public float GetTargetDistance()
        {
            if (null == _target)
                return 0;

            Vector3 distVector = _target.gameObject.transform.position - gameObject.transform.position;
            float dist = distVector.sqrMagnitude;
            return dist;
        }

        public void SetTargetPosition()
        {
            if(null != _target)
            {

            }
        }

        public void Look(GameObject target)
        {
            gameObject.transform.LookAt(target.transform);
        }

        public void SetDestination(Vector3 vec)
        {
            if (null != _navMeshAgent)
            {
                _targetPos = vec;
                _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public void SetDestinationToTarget(GameObject target)
        {
            if (null != _navMeshAgent)
            {
                _target = target;
                _targetPos = target.transform.position;
                _navMeshAgent.SetDestination(_targetPos);
            }
        }

        public void Attack(int damage)
        {
            if (null != _target)
            {
                if(_target.TryGetComponent<Unit>(out Unit unit))
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

        void _RefreshTargetPosition()
        {
            SetDestinationToTarget(_target);
        }
        
        bool _CompareDistance(GameObject obj)
        {
            GameObject nearestObj = Utility.GetNearerObject(gameObject.transform.position, _target, obj);

            if (_target == nearestObj) 
                return false;
            else
            {
                _target = nearestObj;
                base.StateMachine.ChangeState(_idleState);
                return true;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _attackDamage = 1;
            _attackSpeed = 1.0f;
            _attackDistance = 1.0f;
            _lastAttackTime = 0.0f;
            _targetPos = Vector3.zero;
        }

        protected override void Start()
        {
            base.Start();
            _target = null;
            _nearestPlayerBuilding = null;
            _commanderUnit = GameManager.Instance.CommanderObj;
            _idleState = new MonsterIdleState(this);
            _moveState = new MonsterMoveState(this);
            _attackState = new MonsterAttackState(this);
            base.StateMachine.Initialize(_idleState);
        }

        protected override void Update()
        {
            base.Update();
            //_CompareDistance(_playerUnit);
        }
    }

}
