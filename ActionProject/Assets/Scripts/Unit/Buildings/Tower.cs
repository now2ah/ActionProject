using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Util;
using DG.Tweening;

namespace Action.Units
{
    public class Tower : Building
    {
        GameObject _target;
        float _attackDamage;
        float _attackDistance;
        float _attackSpeed;
        ActionTime _attackTime;
        Vector3 _shootPosition;

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

        void _ShootArrow()
        {
            Game.Projectile arrowProj = PoolManager.Instance.ArrowProjectilePool.GetNew();
            Game.Arrow arrow = arrowProj as Game.Arrow;
            arrow.Initialize(this, _attackDamage);
            arrow.Target = _target;
            arrow.transform.LookAt(_target.transform);
            arrow.transform.position = _shootPosition;
            arrow.transform.DOMoveX(_target.transform.position.x, 0.5f).SetEase(Ease.OutQuad);
            arrow.transform.DOMoveY(_target.transform.position.y, 0.5f).SetEase(Ease.InQuad);
            arrow.transform.DOMoveZ(_target.transform.position.z, 0.5f).SetEase(Ease.OutQuad);
        }

        void _Attack()
        {
            _ShootArrow();
            _attackTime.TickStart(_attackSpeed);
        }

        protected override void Awake()
        {
            base.Awake();
            _attackDamage = 1.0f;
            _attackDistance = 150.0f;
            _attackSpeed = 1.0f;
            _attackTime = gameObject.AddComponent<ActionTime>();
            _shootPosition = transform.position + new Vector3(0.0f, 5.0f, 0.0f);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (_IsInDistance() && !_attackTime.IsStarted)
                _Attack();
        }
    }
}

