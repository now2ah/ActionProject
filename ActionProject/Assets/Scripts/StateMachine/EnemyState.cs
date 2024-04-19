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
        NormalEnemy _enemyUnit;
        public EnemyIdleState(NormalEnemy enemyUnit)
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
        NormalEnemy _enemyUnit;

        public EnemyMoveState(NormalEnemy enemyUnit)
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
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.AttackState);
                }
            }
        }
    }

    public class EnemyAttackState : AttackState
    {
        NormalEnemy _enemyUnit;
        public EnemyAttackState(NormalEnemy enemyUnit)
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
            _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
        }
    }

    public class MeleeEnemyIdleState : IdleState
    {
        MeleeEnemy _enemyUnit;
        public MeleeEnemyIdleState(MeleeEnemy enemyUnit)
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

    public class MeleeEnemyMoveState : MoveState
    {
        MeleeEnemy _enemyUnit;

        public MeleeEnemyMoveState(MeleeEnemy enemyUnit)
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

    public class MeleeEnemyAttackState : AttackState
    {
        MeleeEnemy _enemyUnit;
        public MeleeEnemyAttackState(MeleeEnemy enemyUnit)
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
            else if (null == _enemyUnit.Target || _enemyUnit.AttackDistance < _enemyUnit.GetTargetDistance())
                _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
        }
    }
}