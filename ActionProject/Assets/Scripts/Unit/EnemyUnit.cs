using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Action.State;
using Action.Util;
using Action.Manager;

namespace Action.Units
{
    public class EnemyUnit : Unit
    {
        NavMeshAgent _navMeshAgent;

        int _attackDamage;
        
        protected GameObject _target;
        GameObject _nearestPlayerBuilding;
        GameObject _commanderUnit;
        Vector3 _targetPos;

        public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }

        public GameObject Target { get { return _target; } set { _target = value; } }
        public Vector3 TargetPos { get { return _targetPos; } set { _targetPos = value; } }

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.OnRefresh.AddListener(RefreshTargetPosition);
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

        public void FindNearestTarget(bool isIncludeCmd)  /*Commander ���� ����*/
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

            //nearestBuilding�̶� �Ÿ� ��
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

        public virtual void RefreshTargetPosition()
        {
            SetDestinationToTarget(_target);
        }

        protected void _ResetTarget()
        {
            if (!_navMeshAgent.isStopped)
                SetDestination(transform.position);

            _target = null;
        }

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _attackDamage = 1;
            _targetPos = Vector3.zero;
        }

        protected override void Start()
        {
            base.Start();
            _target = null;
            _nearestPlayerBuilding = null;
            _commanderUnit = GameManager.Instance.CommanderObj;
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}