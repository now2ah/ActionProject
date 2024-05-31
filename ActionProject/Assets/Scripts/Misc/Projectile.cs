using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        public int PoolID { get; set; }
        public ObjectPooler<Projectile> Pool { get; set; }
        protected float _speed;

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(5.0f);
            Pool.Free(this);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _speed = 0.2f;
            StartCoroutine(DestroyCoroutine());
        }

        protected virtual void FixedUpdate()
        {

        }
    }

    public class NormalProjectile : Projectile
    {
        void _MoveForward()
        {
            transform.Translate(Vector3.forward * _speed);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _MoveForward();
        }
    }

    public class GuideProjectile : NormalProjectile
    {
        GameObject _target;
        public GameObject Target { get { return _target; } set { _target = value; } }

        void _FollowTarget()
        {
            if (null != _target)
                transform.LookAt(_target.transform);
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

