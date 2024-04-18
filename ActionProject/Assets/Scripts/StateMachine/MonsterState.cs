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
                _monsterUnit.FindNearestPlayerBuilding();
                _monsterUnit.FindNearestTarget(true);
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
                if (null == _monsterUnit.Target)
                    return;


                if (null != _monsterUnit.Target)
                    _monsterUnit.StateMachine.ChangeState(_monsterUnit.MoveState);

                _monsterUnit.FindNearestTarget(true);
            }
        }
    }

    public class MonsterMoveState : MoveState
    {
        MonsterUnit _monsterUnit;

        public MonsterMoveState(MonsterUnit monsterUnit)
        {
            _monsterUnit = monsterUnit;
        }

        public override void EnterState()
        {
            if (null != _monsterUnit && null != _monsterUnit.Target)
            {
                _monsterUnit.SetDestinationToTarget(_monsterUnit.Target);
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
                if (Vector3.Distance(_monsterUnit.transform.position, _monsterUnit.TargetPos) < 1.0f)
                {
                    if (_monsterUnit.AttackDistance > _monsterUnit.GetTargetDistance())
                        _monsterUnit.StateMachine.ChangeState(_monsterUnit.AttackState);
                    else
                        _monsterUnit.StateMachine.ChangeState(_monsterUnit.IdleState);
                }
            }
        }
    }

    public class MonsterAttackState : AttackState
    {
        MonsterUnit _monsterUnit;
        public MonsterAttackState(MonsterUnit monsterUnit)
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