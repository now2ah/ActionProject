using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Game
{
    public class DirectionalAttack : AutoAttackAbilty
    {
        protected override void _AutoAttack()
        {
            if (null != AttackTime)
            {
                if (!AttackTime.IsStarted)
                {
                    //attack logic
                    _CreateProjectile();
                    AttackTime.TickStart(3.0f);
                }

                if (AttackTime.IsFinished)
                    AttackTime.ResetTimer();
            }
        }

        void _CreateProjectile()
        {
            Vector3 shootPosition = transform.position + transform.forward * 2.0f;
            shootPosition.y = Constant.PROJECTILE_Y_POS;
            NormalProjectile projectile = (NormalProjectile)PoolManager.Instance.NormalProjectilePool.GetNew();
            projectile.transform.position = shootPosition;
            projectile.transform.rotation = transform.rotation;
            projectile.Owner = this.gameObject;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}

