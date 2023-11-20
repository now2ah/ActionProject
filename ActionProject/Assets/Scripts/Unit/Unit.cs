using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class Unit : MonoBehaviour
    {
        int hp = 0;
        public int HP { get { return hp; } set { hp = value; } }
        int fullHp = 0;
        public int FullHp { get { return fullHp; } set { fullHp = value; } }
        
        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public void GetDamaged(int damage)
        {
            hp -= damage;
            if (hp < 0)
                _Death();
        }

        void _Death()
        {

        }

        public virtual void Start()
        {
            _stateMachine = new StateMachine();
        }

        public virtual void Update()
        {
            if(null != _stateMachine)
                _stateMachine.Update();
        }
    }
}