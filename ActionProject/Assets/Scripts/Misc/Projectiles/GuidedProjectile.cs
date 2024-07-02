using Action.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using DG.Tweening;

namespace Action.Game
{
    public class GuidedProjectile : NormalProjectile
    {
        GameObject _target;
        float _startSpeed;
        float _activeDistance;

        public GameObject Target { get { return _target; } set { _target = value; } }

        public override void Initialize(Unit owner, float attackDamage)
        {
            base.Initialize(owner, attackDamage);
            _target = _GetNearestTarget();
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

            if (null != nearestTarget && nearestTarget.TryGetComponent<EnemyUnit>(out EnemyUnit comp))
            {
                if (comp.IsDead)
                    return null;
            }

            if (null != nearestTarget)
            {
                float distance = Vector3.Distance(nearestTarget.transform.position, gameObject.transform.position);
                if (distance > _activeDistance)
                    return null;
            }

            return nearestTarget;
        }

        void _FollowTargetMove()
        {
            if (null != _target)
                transform.LookAt(_target.transform.position);
        }

        protected override void _MoveForward()
        {
            if (_startSpeed <= _speed)
                _startSpeed += 0.1f;

            transform.Translate(Vector3.forward * _startSpeed);
        }

        private void Awake()
        {
            _startSpeed = 0.0f;
            _activeDistance = 20.0f;
        }

        protected new void Start()
        {
            base.Start();
        }

        protected new void FixedUpdate()
        {
            _FollowTargetMove();
            _MoveForward();
        }
    }
}

