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
            playerUnit.IsMoving = true;
            playerUnit.Animator.SetBool("isMoving", playerUnit.IsMoving);
        }

        public override void ExitState()
        {
            //Debug.Log("Exit Idle");
            playerUnit.IsMoving = false;
            playerUnit.Animator.SetBool("isMoving", playerUnit.IsMoving);
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
            if (null != playerUnit)
                playerUnit.Move();
        }
    }

    public class PlayerBuildingIdleState : IdleState
    {
        PlayerBuilding playerBuilding;

        public PlayerBuildingIdleState(PlayerBuilding playerBuilding)
        {
            this.playerBuilding = playerBuilding;
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
}