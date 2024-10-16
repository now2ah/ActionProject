using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action;

namespace Action.State
{
    public class StateMachine
    {
        State _CurState;
        public State CurState => _CurState;
        bool _IsRunning = true;
        public bool IsRunning { get { return _IsRunning; } set { _IsRunning = value; } }

        public virtual void Initialize(State startState)
        {
            if (null == _CurState)
                _CurState = new State();
            ChangeState(startState);
            _IsRunning = true;
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
            if (_IsRunning)
                _CurState.UpdateState();
        }
    }
}

