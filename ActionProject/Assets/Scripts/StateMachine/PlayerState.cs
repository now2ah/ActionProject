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

    public class PlayerMoveState : MoveState
    {
        PlayerUnit playerUnit;

        public PlayerMoveState(PlayerUnit playerUnit)
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
        Building _building;

        public PlayerBuildingIdleState(Building building)
        {
            this._building = building;
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

    public class PlayerBuildingPrepareState : State
    {
        Building _building;

        public PlayerBuildingPrepareState(Building building)
        {
            this._building = building;
        }

        public override void EnterState()
        {
            Logger.Log("prepare");
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {

        }
    }

    public class PlayerBuildingDoneState : State
    {
        Building _building;

        public PlayerBuildingDoneState(Building building)
        {
            this._building = building;
        }

        public override void EnterState()
        {
            Logger.Log("done");
            _building.SetVisibleBuilding(true);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {

        }
    }

    public class PlayerBuildingCollapseState : State
    {
        Building _building;

        public PlayerBuildingCollapseState(Building building)
        {
            this._building = building;
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {

        }
    }
}