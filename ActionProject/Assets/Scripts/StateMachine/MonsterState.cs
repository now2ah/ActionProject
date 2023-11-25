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
                _monsterUnit.NearestTarget = _monsterUnit.FindNearestTarget();
                if (null == _monsterUnit.NearestTarget || null == GameManager.Instance.PlayerBase)
                    return;
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
            {
                _monsterUnit.Look(_monsterUnit.Target);
            }
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _monsterUnit)
            {
                _monsterUnit.Look(_monsterUnit.Target);
                _monsterUnit.Move();

                if (_monsterUnit.AttackDistance > _monsterUnit.GetTargetDistance())
                    _monsterUnit.StateMachine.ChangeState(_monsterUnit.AttackingState);
            }
        }
    }

    public class MonsterAttackingState : AttackingState
    {
        MonsterUnit _monsterUnit;
        public MonsterAttackingState(MonsterUnit monsterUnit)
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
            if (null != _monsterUnit && null != _monsterUnit.Target && !_monsterUnit.isAttackCooltime())
                _monsterUnit.Attack(_monsterUnit.AttackDamage);
            else if(null == _monsterUnit.Target || _monsterUnit.AttackDistance < _monsterUnit.GetTargetDistance())
                _monsterUnit.StateMachine.ChangeState(_monsterUnit.IdleState);
        }
    }
}