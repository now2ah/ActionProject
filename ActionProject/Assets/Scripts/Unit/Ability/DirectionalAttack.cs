using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Manager;

namespace Action.Game
{
    public class DirectionalAttack : AutoAttackAbility
    {
        public override void LevelUp(int level)
        {
            base.LevelUp(level);
            if (1 < level)
            {
                switch (level)
                {
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void _AutoAttack()
        {
            if (null != AttackTimer)
            {
                if (!AttackTimer.IsStarted)
                {
                    //attack logic
                    _CreateProjectile(AttackDamage);
                    AttackTimer.TickStart(AttackPeriod);
                }

                if (AttackTimer.IsFinished)
                    AttackTimer.ResetTimer();
            }
        }

        void _CreateProjectile(float attackDamage)
        {
            Vector3 shootPosition = transform.position + transform.forward * 2.0f;
            shootPosition.y = GameManager.Instance.Constants.HUNT_PROJECTILE_Y_POS;
            NormalProjectile projectile = (NormalProjectile)PoolManager.Instance.NormalProjectilePool.GetNew();
            projectile.transform.position = shootPosition;
            projectile.transform.rotation = transform.rotation;
            if (gameObject.TryGetComponent<Unit>(out Unit comp))
                projectile.Initialize(comp, attackDamage);
        }

        protected new void Awake()
        {
            base.Awake();
            AbilityName = "Bullet";
            Description = "Shooting projectile to forward direction.";
            //default
            AttackPeriod = 1.0f;
            AttackDamage = 5.0f;
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

