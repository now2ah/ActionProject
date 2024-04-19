using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.Units;
using Action.Manager;
using Action.Util;

namespace Action.State
{
    public class EnemyIdleState : IdleState
    {
        EnemyUnit _enemyUnit;
        public EnemyIdleState(EnemyUnit enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public override void EnterState()
        {
            if (null != _enemyUnit)
            {
                _enemyUnit.FindNearestPlayerBuilding();
                _enemyUnit.FindNearestTarget(true);
            }
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (null != _enemyUnit)
            {
                if (null == _enemyUnit.Target)
                    return;


                if (null != _enemyUnit.Target)
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.MoveState);

                _enemyUnit.FindNearestTarget(true);
            }
        }
    }

    public class EnemyMoveState : MoveState
    {
        EnemyUnit _enemyUnit;

        public EnemyMoveState(EnemyUnit enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public override void EnterState()
        {
            if (null != _enemyUnit && null != _enemyUnit.Target)
            {
                _enemyUnit.SetDestinationToTarget(_enemyUnit.Target);
            }
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _enemyUnit)
            {
                if (Vector3.Distance(_enemyUnit.transform.position, _enemyUnit.TargetPos) < 1.0f)
                {
                    if (_enemyUnit.AttackDistance > _enemyUnit.GetTargetDistance())
                        _enemyUnit.StateMachine.ChangeState(_enemyUnit.AttackState);
                    else
                        _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
                }
            }
        }
    }

    public class EnemyAttackState : AttackState
    {
        EnemyUnit _enemyUnit;
        public EnemyAttackState(EnemyUnit enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }
        public override void EnterState()
        {
            if (null != _enemyUnit)
                _enemyUnit.Look(_enemyUnit.Target);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _enemyUnit && null != _enemyUnit.Target && !_enemyUnit.isAttackCooltime())
                _enemyUnit.Attack(_enemyUnit.AttackDamage);
            else if(null == _enemyUnit.Target || _enemyUnit.AttackDistance < _enemyUnit.GetTargetDistance())
                _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
        }
    }
}