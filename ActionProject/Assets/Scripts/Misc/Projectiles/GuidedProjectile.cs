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
        Sequence _moveSequence;
        bool _isReady;

        public GameObject Target { get { return _target; } set { _target = value; } }

        public override void Initialize(Unit owner, float attackDamage)
        {
            base.Initialize(owner, attackDamage);
            _target = _GetNearestTarget();
            _moveSequence.Play();
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

        void _FollowTargetMove()
        {
            if (null != _target)
                transform.LookAt(_target.transform.position);
            else
                transform.LookAt(transform.forward);
        }

        void _SetReady(bool isReady)
        {
            _isReady = isReady;
        }

        private void Awake()
        {
            _isReady = false;
        }

        protected new void Start()
        {
            base.Start();
            _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOMove(_target.transform.position, 1.0f).SetSpeedBased());
            _moveSequence.onComplete += ( () => { _isReady = true; });
        }

        protected new void FixedUpdate()
        {
            if(_isReady)
            {
                _FollowTargetMove();
                _MoveForward();
            }
        }
    }
}

