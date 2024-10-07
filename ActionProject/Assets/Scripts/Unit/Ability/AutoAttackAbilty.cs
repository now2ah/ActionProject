using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Game
{
    public class AutoAttackAbility : Ability
    {
        float _attackPeriod;
        float _attackDamage;
        ActionTime _attackTime;
        
        public float AttackPeriod { get { return _attackPeriod; } set { _attackPeriod = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public ActionTime AttackTimer { get { return _attackTime; } set { _attackTime = value; } }

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
            _attackPeriod = 0.0f;
            _attackDamage = 0.0f;
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
        }

        private void FixedUpdate()
        {
            if (IsActivated)
                _AutoAttack();
        }
    }
}

