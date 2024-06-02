using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Game
{
    public class DirectionalAttack : AutoAttackAbilty
    {
        ObjectPooler<Projectile> _projectilePool;

        protected override void _AutoAttack()
        {
            if (null != AttackTime)
            {
                if (!AttackTime.IsStarted)
                {
                    //attack logic
                    Logger.Log(AttackTime.GetTimeString());
                    AttackTime.TickStart(3.0f);
                }

                if (AttackTime.IsFinished)
                    AttackTime.ResetTimer();
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _projectilePool = new ObjectPooler<Projectile>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}

