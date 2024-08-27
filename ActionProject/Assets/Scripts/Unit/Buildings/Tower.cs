using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Util;

namespace Action.Units
{
    public class Tower : Building
    {
        GameObject _target;
        float _attackDistance;
        float _attackSpeed;
        ActionTime _attackTime;

        public override void Initialize()
        {
            base.Initialize();
            GameManager.Instance.OnRefresh.AddListener(_FindTarget);
        }

        void _FindTarget()
        {
            float nearestDist = 10000f;
            for (int i=0; i<GameManager.Instance.EnemyUnits.Count; i++)
            {
                float dist = Vector3.Distance(transform.position, GameManager.Instance.EnemyUnits[i].transform.position);
                if (nearestDist > dist)
                {
                    _target = GameManager.Instance.EnemyUnits[i];
                    nearestDist = dist;
                }
            }
        }

        bool _IsInDistance()
        {
            if (null == _target)
                return false;

            if (_attackDistance > Vector3.Distance(transform.position, _target.transform.position))
                return true;
            else
                return false;
        }

        void _Attack()
        {
            Logger.Log("Attack");
            _attackTime.TickStart(_attackSpeed);
        }

        protected override void Awake()
        {
            base.Awake();
            _attackDistance = 15.0f;
            _attackSpeed = 1.0f;
            _attackTime = gameObject.AddComponent<ActionTime>();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (_IsInDistance() && !_attackTime.IsStarted)
                _Attack();
        }
    }
}

