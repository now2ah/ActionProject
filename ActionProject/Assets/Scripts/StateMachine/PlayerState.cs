using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Units;
using Action.Manager;

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
            
        }

        public override void ExitState()
        {
            base.ExitState();
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
            if (null != commander)
            {
                commander.IsMoving = true;
                commander.Animator.SetBool(commander.AnimHashMoving, commander.IsMoving);
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            if (null != commander)
            {
                commander.IsMoving = false;
                commander.Animator.SetBool(commander.AnimHashMoving, commander.IsMoving);
            }
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
            if (null != commander && !commander.IsAttacking)
            {
                commander.PhysicalAttack();
            }
        }

        public override void ExitState()
        {
            base.ExitState();
            if (null != commander)
            {
            }
        }

        // Update is called once per frame
        public override void UpdateState()
        {
            base.UpdateState();
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
        }

        public override void ExitState()
        {
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
            _building.SetVisibleBuilding(true);
            _building.SetMaterial();
            //interaction 없을때 if 문 추가
            _building.ControlUI.Hide();
            _building.IsOnUnitPanel = true;
            BuildingData data = _building.UnitData as BuildingData;
            data.isBuilt = true;
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
            _building.SetVisibleBuilding(false);
            BuildingData data = _building.UnitData as BuildingData;
            data.isBuilt = false;
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.COLLAPSE);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {

        }
    }
}