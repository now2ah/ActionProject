using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Units;

namespace Action.Game
{
    public class NormalProjectile : Projectile
    {
        public override void Initialize(Unit owner, float attackDamage)
        {
            base.Initialize(owner, attackDamage);

        }

        protected virtual void _MoveForward()
        {
            transform.Translate(Vector3.forward * _speed);
        }

        protected override void OnCollisionEnter(Collision col)
        {
            base.OnCollisionEnter(col);
            if ("EnemyObject" == col.gameObject.tag)
            {
                Pool.Free(this);
                Unit.DamageMessage msg = new Unit.DamageMessage
                {
                    damager = Owner,
                    amount = AttackDamage
                };

                if (col.transform.TryGetComponent<Unit>(out Unit comp))
                    comp.ApplyDamage(msg);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected new void Start()
        {
            base.Start();
        }

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
            _MoveForward();
        }
    }
}

