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
        
        public bool IsState(State state)
        {
            if (null == state)
                return false;

            if (state == _CurState)
                return true;
            else
                return false;
        }

        private void Update()
        {
            _CurState.UpdateState();
        }
    }
}

