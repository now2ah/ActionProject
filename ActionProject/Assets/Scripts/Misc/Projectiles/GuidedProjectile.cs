using Action.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Game
{
    public class GuidedProjectile : NormalProjectile
    {
        GameObject _target;
        public GameObject Target { get { return _target; } set { _target = value; } }

        public override void Initialize(Unit owner, float attackDamage)
        {
            base.Initialize(owner, attackDamage);
            _target = _GetNearestTarget();
        }

        void _FollowTarget()
        {
            if (null != _target)
                transform.LookAt(_target.transform);
        }

        GameObject _GetNearestTarget()
        {
            float nearest = Mathf.Infinity;
            GameObject nearestTarget = null;
            for (int i = 0; i < GameManager.Instance.EnemyUnits.Count; i++)
            {
                Vector3 dist = GameManager.Instance.EnemyUnits[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    nearestTarget = GameManager.Instance.EnemyUnits[i];
                }
            }
            return nearestTarget;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _FollowTarget();
        }
    }
}

