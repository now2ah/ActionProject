using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Units;

namespace Action.State
{
    public class PlayerIdleState : IdleState
    {
        PlayerUnit playerUnit;

        public PlayerIdleState(PlayerUnit playerUnit)
        {
            this.playerUnit = playerUnit;
        }

        public override void EnterState()
        {
            //Debug.Log("Enter Idle");
        }

        public override void ExitState()
        {
            //Debug.Log("Exit Idle");
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class PlayerMovingState : MovingState
    {
        PlayerUnit playerUnit;

        public PlayerMovingState(PlayerUnit playerUnit)
        {
            this.playerUnit = playerUnit;
        }

        public override void EnterState()
        {
            //Debug.Log("Enter Idle");
        }

        public override void ExitState()
        {
            //Debug.Log("Exit Idle");
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
            if (null != playerUnit)
                playerUnit.Move();
        }
    }
}