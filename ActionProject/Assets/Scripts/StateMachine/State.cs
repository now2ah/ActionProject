using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.State
{
    public class State
    {
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void UpdateState() { }
    }

    public class IdleState : State
    {
        public override void EnterState()
        {
            Debug.Log("Enter Idle");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Idle");
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class MovingState : State
    {
        public override void EnterState()
        {
            Debug.Log("Enter Moving");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Moving");
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();

        }
    }

    public class AttackingState : State
    {
        public override void EnterState()
        {
            Debug.Log("Enter Moving");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Moving");
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();

        }
    }
}
