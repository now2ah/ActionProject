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
        
        public bool IsActivated { get { return _isActivated; } set { _isActivated = value; } }
        public float AttackPeriod { get { return _attackPeriod; } set { _attackPeriod = value; } }
        public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }

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

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            _AutoAttack();
        }
    }
}

