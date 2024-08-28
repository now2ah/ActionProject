using Action.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Arrow : Projectile
    {
        GameObject _target;
        public GameObject Target { get { return _target; } set { _target = value; } }

        public override void Initialize(Unit owner, float attackDamage)
        {
            base.Initialize(owner, attackDamage);

        }

        protected override void Awake()
        {
            base.Awake();
            _speed = 1.0f;
            _attackDamage = 1.0f;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (null != _target)
                transform.LookAt(_target.transform);
        }
    }
}
