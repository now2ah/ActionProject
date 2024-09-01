using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Units;

namespace Action.Game
{
    public class RangeEnemyProjectile : Projectile
    {
        void _MoveForward()
        {
            transform.Translate(Vector3.forward * _speed);
        }

        protected override void OnCollisionEnter(Collision col)
        {
            base.OnCollisionEnter(col);
            
        }

        protected override void OnTriggerEnter(Collider other)
        {
           base.OnTriggerEnter(other);
            if ("PlayerObject" == other.transform.tag)
            {
                Pool.Free(this);
                Unit.DamageMessage msg = new Unit.DamageMessage
                {
                    damager = Owner,
                    amount = AttackDamage
                };

                if (other.transform.TryGetComponent<Unit>(out Unit comp))
                    comp.ApplyDamage(msg);
            }
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
}
