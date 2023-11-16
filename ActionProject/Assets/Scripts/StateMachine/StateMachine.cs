using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

namespace Action.State
{
    public class StateMachine
    {
        State _CurState;

        public virtual void Initialize(State startState)
        {
            _CurState = new State();
            ChangeState(startState);
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

        public void Update()
        {
            _CurState.UpdateState();
        }
    }
}

