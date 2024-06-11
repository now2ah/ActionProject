using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

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
                Logger.Log("EnemyHit");
                Pool.Free(this);
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

