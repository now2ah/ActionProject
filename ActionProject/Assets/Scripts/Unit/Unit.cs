using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class Unit : MonoBehaviour
    {
        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

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