using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Util;
using Action.Manager;

namespace Action.Units
{
    public class MonsterUnit : Unit
    {
        int _attackDamage = 0;
        float _attackSpeed = 0.0f;
        float _attackDistance = 0.0f;
        float _lastAttackTime = 0.0f;
        MonsterIdleState _idleState;
        MonsterMovingState _movingState;
        MonsterAttackingState _attackingState;
        GameObject _target;
        GameObject _nearestTarget;
        GameObject _playerUnit;

        public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
        public MonsterIdleState IdleState => _idleState;
        public MonsterMovingState MovingState => _movingState;
        public MonsterAttackingState AttackingState => _attackingState;
        public GameObject Target { get { return _target; } set { _target = value; } }
        public GameObject NearestTarget { get { return _nearestTarget; } set { _nearestTarget = value; } }

        public GameObject FindNearestTarget()
        {
            GameObject nearestObj = null;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerObject");

            if (objs.Length == 0)
                return null;

            float nearest = Mathf.Infinity;
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] == GameManager.Instance.PlayerUnit)
                    continue;

                Vector3 dist = objs[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    nearestObj = objs[i];
                }
            }

            //base¶û °Å¸® ºñ±³
            GameObject target = Utility.GetNearerObject(gameObject.transform.position, nearestObj, GameManager.Instance.PlayerBase);
            nearestObj = target;

            return nearestObj;
        }

        public float GetTargetDistance()
        {
            if (null == _target)
                return 0;

            Vector3 distVector = _target.gameObject.transform.position - gameObject.transform.position;
            float dist = distVector.sqrMagnitude;
            return dist;
        }

        public void Move()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5.0f, Space.Self);
        }

        public void Look(GameObject target)
        {
            gameObject.transform.LookAt(target.transform);
        }

        public void Attack(int damage)
        {
            if (null != _target)
            {
                if(_target.TryGetComponent<Unit>(out Unit unit))
                {
                    unit.GetDamaged(damage);
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

        
        bool _CompareDistance(GameObject obj)
        {
            GameObject nearestObj = Utility.GetNearerObject(gameObject.transform.position, _nearestTarget, obj);

            if (_target == nearestObj) 
                return false;
            else
            {
                _target = nearestObj;
                base.StateMachine.ChangeState(_idleState);
                return true;
            }
        }

        public override void Start()
        {
            base.Start();
            _target = null;
            _playerUnit = GameManager.Instance.PlayerUnit;
            _idleState = new MonsterIdleState(this);
            _movingState = new MonsterMovingState(this);
            _attackingState = new MonsterAttackingState(this);
            base.StateMachine.Initialize(_idleState);
            _attackSpeed = 1.0f;
            _attackDistance = 2.0f;
        }

        public override void Update()
        {
            base.Update();
            _CompareDistance(_playerUnit);
        }
    }

}
