using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Util;

namespace Action.Game
{
    public abstract class Projectile : MonoBehaviour, IPoolable<Projectile>
    {
        protected Unit _owner;
        protected float _speed;
        protected float _attackDamage;

        public int PoolID { get; set; }
        public ObjectPooler<Projectile> Pool { get; set; }
        public Unit Owner { get { return _owner; } set { _owner = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }

        public void Initialize(Unit owner, float attackDamage)
        {
            _owner = owner;
            _attackDamage = attackDamage;
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(4.0f);
            Pool.Free(this);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(DestroyCoroutine());
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _speed = 0.2f;
        }

        protected virtual void FixedUpdate()
        {

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
