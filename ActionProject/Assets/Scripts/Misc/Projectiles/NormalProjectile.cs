using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Units;

namespace Action.Game
{
    public class NormalProjectile : Projectile
    {
        void _MoveForward()
        {
            transform.Translate(Vector3.forward * _speed);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if ("EnemyObject" == other.gameObject.tag)
            {
                Pool.Free(this);
                Unit.DamageMessage msg = new Unit.DamageMessage
                {
                    damager = Owner,
                    amount = AttackDamage
                };

                if (other.TryGetComponent<Unit>(out Unit comp))
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

