using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Game
{
    public class AutoAttackAbilty : MonoBehaviour
    {
        bool _isActivated;
        float _attackPeriod;
        float _attackDamage;
        ActionTime _attackTime;
        
        public bool IsActivated { get { return _isActivated; } set { _isActivated = value; } }
        public float AttackPeriod { get { return _attackPeriod; } set { _attackPeriod = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
        public ActionTime AttackTimer { get { return _attackTime; } set { _attackTime = value; } }

        protected virtual void _AutoAttack()
        {

        }

        protected virtual void Awake()
        {
            _isActivated = false;
            _attackPeriod = 0.0f;
            _attackDamage = 0.0f;
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            AttackTimer = gameObject.AddComponent<ActionTime>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            
        }

        private void FixedUpdate()
        {
            if (_isActivated)
                _AutoAttack();
        }
    }
}

