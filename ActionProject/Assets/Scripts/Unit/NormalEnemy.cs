using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class NormalEnemy : EnemyUnit
    {
        EnemyIdleState _idleState;
        EnemyMoveState _moveState;
        EnemyAttackState _attackState;

        public EnemyIdleState IdleState => _idleState;
        public EnemyMoveState MoveState => _moveState;
        public EnemyAttackState AttackState => _attackState;

        protected override void Start()
        {
            base.Start();
            _idleState = new EnemyIdleState(this);
            _moveState = new EnemyMoveState(this);
            _attackState = new EnemyAttackState(this);
            base.StateMachine.Initialize(_idleState);
        }
    }
}

