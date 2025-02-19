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
                _enemyUnit.Stop();
                _enemyUnit.FindNearestPlayerBuilding();
                _enemyUnit.FindNearestTarget(true);
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, false);
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

                //_enemyUnit.FindNearestTarget(true);
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
                if (_enemyUnit.NavMeshAgentComp.isOnNavMesh)
                    _enemyUnit.SetDestinationToTarget(_enemyUnit.Target);
                _enemyUnit.Look(_enemyUnit.Target);
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, true);
            }
        }

        public override void ExitState()
        {
            _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, false);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _enemyUnit)
            {
                _enemyUnit.Look(_enemyUnit.Target);
                if (_enemyUnit.isTargetInDistance())
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.AttackState);
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
            {
                _enemyUnit.Stop();
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, false);
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashAttacking, true);
                _enemyUnit.Look(_enemyUnit.Target);
            } 
        }

        public override void ExitState()
        {
            _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashAttacking, false);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _enemyUnit && null != _enemyUnit.Target)
            {
                _enemyUnit.Look(_enemyUnit.Target);

                if (!_enemyUnit.isAttackCooltime())
                    _enemyUnit.Attack();

                if (!_enemyUnit.isTargetInDistance())
                {
                    _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashAttacking, false);
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
                }
            }
        }
    }

    public class RangeEnemyIdleState : IdleState
    {
        RangeEnemy _enemyUnit;
        public RangeEnemyIdleState(RangeEnemy enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public override void EnterState()
        {
            if (null != _enemyUnit)
            {
                //Logger.Log("Idle Enter");
                if (_enemyUnit.NavMeshAgentComp.isOnNavMesh)
                    _enemyUnit.StopAgent();
                _enemyUnit.FindNearestPlayerBuilding();
                _enemyUnit.FindNearestTarget(true);
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, false);
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
                //Logger.Log("Idle Update");
                if (null == _enemyUnit.Target)
                    return;

                if (null != _enemyUnit.Target)
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.MoveState);

                //_enemyUnit.FindNearestTarget(true);
            }
        }
    }

    public class RangeEnemyMoveState : MoveState
    {
        RangeEnemy _enemyUnit;

        public RangeEnemyMoveState(RangeEnemy enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public override void EnterState()
        {
            base.EnterState();
            if (null != _enemyUnit && null != _enemyUnit.Target)
            {
                //Logger.Log("Move Enter");
                if (_enemyUnit.NavMeshAgentComp.isOnNavMesh)
                    _enemyUnit.SetDestinationToTarget(_enemyUnit.Target);
                _enemyUnit.Look(_enemyUnit.Target);
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, true);
            }
        }

        public override void ExitState()
        {
            _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashMoving, false);
        }

        public override void UpdateState()
        {
            base.UpdateState();
            if (null != _enemyUnit)
            {
                _enemyUnit.Look(_enemyUnit.Target);
                //Logger.Log("Move Update");
                if (_enemyUnit.isTargetInDistance())
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.AttackState);
            }
        }
    }

    public class RangeEnemyAttackState : AttackState
    {
        RangeEnemy _enemyUnit;
        public RangeEnemyAttackState(RangeEnemy enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }
        public override void EnterState()
        {
            if (null != _enemyUnit)
            {
                //Logger.Log("Attack Enter");
                if (_enemyUnit.NavMeshAgentComp.isOnNavMesh)
                    _enemyUnit.Stop();
                _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashAttacking, true);
                _enemyUnit.Look(_enemyUnit.Target);
            }
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            base.UpdateState();
            
            if (null != _enemyUnit && null != _enemyUnit.Target)
            {
                if (_enemyUnit.NavMeshAgentComp.isOnNavMesh)
                    _enemyUnit.Stop();
                _enemyUnit.Look(_enemyUnit.Target);

                if (!_enemyUnit.isAttackCooltime())
                {
                    EnemyUnitData data = _enemyUnit.UnitData as EnemyUnitData;
                    _enemyUnit.Attack(data.attackDamage);
                }
                    
                
                if (!_enemyUnit.isTargetInDistance())
                {
                    _enemyUnit.Animator.SetBool(_enemyUnit.AnimHashAttacking, false);
                    _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
                }
                    
            }
            //else if (null == _enemyUnit.Target || _enemyUnit.AttackDistance < _enemyUnit.GetTargetDistance())
            //    _enemyUnit.StateMachine.ChangeState(_enemyUnit.IdleState);
        }
    }
}