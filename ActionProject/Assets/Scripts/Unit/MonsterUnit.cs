using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

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

        public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }
        public MonsterIdleState IdleState => _idleState;
        public MonsterMovingState MovingState => _movingState;
        public MonsterAttackingState AttackingState => _attackingState;
        public GameObject Target { get { return _target; } 
            set { _target = value; } }

        public GameObject FindNearestTarget()
        {
            GameObject nearestObj = null;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerObject");

            if (objs.Length == 0)
                return null;

            float nearest = Mathf.Infinity;
            for (int i = 0; i < objs.Length; i++)
            {
                Vector3 dist = objs[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    nearestObj = objs[i];
                }
            }

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
                    Debug.Log("Attack! : " + damage);
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

        public override void Start()
        {
            base.Start();
            _target = null;
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

        }
    }

}
