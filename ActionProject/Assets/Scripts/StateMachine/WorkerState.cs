using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;

namespace Action.State
{
    public class WorkerIdleState : IdleState
    {
        WorkerUnit workerUnit;

        public WorkerIdleState(WorkerUnit workerUnit)
        {
            this.workerUnit = workerUnit;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class WorkerMoveState : MoveState
    {
        WorkerUnit workerUnit;

        public WorkerMoveState(WorkerUnit workerUnit)
        {
            this.workerUnit = workerUnit;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }

    public class WorkerWorkState : State
    {
        WorkerUnit workerUnit;

        public WorkerWorkState(WorkerUnit workerUnit)
        {
            this.workerUnit = workerUnit;
        }

        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public override void UpdateState()
        {
            base.UpdateState();
        }
    }
}