using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Game
{
    public class AutoAttackAbility : Ability
    {
        ActionTime _attackTimer;
        public ActionTime AttackTimer { get { return _attackTimer; } set { _attackTimer = value; } }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected virtual void _AutoAttack()
        {

        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            AttackTimer = gameObject.AddComponent<ActionTime>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (abilityData.isActivated)
                _AutoAttack();
        }
    }
}

