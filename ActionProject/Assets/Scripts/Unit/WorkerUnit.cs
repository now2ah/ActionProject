using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class WorkerUnit : Unit
    {
        WorkerIdleState _idleState;
        WorkerMoveState _moveState;
        WorkerWorkState _workState;

        //animator
        Animator _animator;
        public Animator Animator => _animator;

        public void SeekJobs()
        {

        }

        public void Move(Vector3 position)
        {
            transform.LookAt(position);
            transform.Translate(position * Speed * Time.deltaTime);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            _idleState = new WorkerIdleState(this);
            _moveState = new WorkerMoveState(this);
            _workState = new WorkerWorkState(this);
            base.StateMachine.Initialize(_idleState);
        }

        // Update is called once per frame
        protected override void Update()
        {

        }
    }

}