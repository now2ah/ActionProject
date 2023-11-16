using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Units;
using Action.Manager;
using Action.Util;

namespace Action.State
{
    public class MonsterIdleState : IdleState
    {
        MonsterUnit _monsterUnit;
        public MonsterIdleState(MonsterUnit monsterUnit)
        {
            _monsterUnit = monsterUnit;
        }

        public override void EnterState()
        {
            if (null != _monsterUnit)
            {
                _monsterUnit.Target = _monsterUnit.FindNearestTarget();

                if (null == _monsterUnit.Target || null == GameManager.Instance.PlayerBase)
                    return;

                //base¶û °Å¸® ºñ±³
                GameObject target = Utility.GetNearerObject(_monsterUnit.transform.position, _monsterUnit.Target, GameManager.Instance.PlayerBase);
                _monsterUnit.Target = target;
            }
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _monsterUnit.Target)
                _monsterUnit.StateMachine.ChangeState(_monsterUnit.MovingState);
        }
    }

    public class MonsterMovingState : MovingState
    {
        MonsterUnit _monsterUnit;
        public MonsterMovingState(MonsterUnit monsterUnit)
        {
            _monsterUnit = monsterUnit;
        }
        public override void EnterState()
        {
            if (null != _monsterUnit)
                _monsterUnit.Look(_monsterUnit.Target);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _monsterUnit)
                _monsterUnit.Move();
        }
    }
}