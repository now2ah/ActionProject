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
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
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
            base.EnterState();
            playerUnit.IsMoving = true;
            playerUnit.Animator.SetBool("isMoving", playerUnit.IsMoving);
        }

        public override void ExitState()
        {
            base.ExitState();
            playerUnit.IsMoving = false;
            playerUnit.Animator.SetBool("isMoving", playerUnit.IsMoving);
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
            //if (null != playerUnit)
            //    playerUnit.Move();
        }
    }

    public class CommanderIdleState : IdleState
    {
        Commander commander;

        public CommanderIdleState(Commander commander)
        {
            this.commander = commander;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class CommanderMoveState : MoveState
    {
        Commander commander;

        public CommanderMoveState(Commander commander)
        {
            this.commander = commander;
        }

        public override void EnterState()
        {
            base.EnterState();

            commander.IsMoving = true;
            commander.Animator.SetBool("isMoving", commander.IsMoving);
        }

        public override void ExitState()
        {
            base.ExitState();

            commander.IsMoving = false;
            commander.Animator.SetBool("isMoving", commander.IsMoving);
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
            if (null != commander)
                commander.Move();
        }
    }

    public class CommanderAttackState : AttackState
    {
        Commander commander;

        public CommanderAttackState(Commander commander)
        {
            this.commander = commander;
        }

        public override void EnterState()
        {
            base.EnterState();
            if (null != commander)
                commander.PhysicalAttack();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();

            if (null != commander)
            {
                commander.Move();
                commander.StateMachine.ChangeState(commander.IdleState);
            }
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
            _building.StartConstructTimer();
            _building.ControlUI.Hide();
            _building.BuildButtonUI.Hide();
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

            //interaction 없을때 if 문 추가
            _building.ControlUI.Hide();

            _building.IsOnUnitPanel = true;
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