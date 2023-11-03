using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.State
{
    public class MonsterIdleState : IdleState
    {
        public override void EnterState()
        {
            Debug.Log("Enter Monster Idle");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Monster Idle");
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class MonsterMovingState : MovingState
    {
    }
}