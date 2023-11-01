using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

namespace Action.State
{
    public class StateMachine : MonoBehaviour
    {
        State _CurState;

        public virtual void Initialize()
        {
            _CurState = new State();
        }

        public virtual void ChangeState(State nextState)
        {
            _CurState.ExitState();
            _CurState = nextState;
            _CurState.EnterState();
        }

        private void Update()
        {
            _CurState.Update();
        }
    }
}

