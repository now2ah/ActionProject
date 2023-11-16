using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class MonsterUnit : Unit
    {
        MonsterIdleState _idleState;
        public MonsterIdleState IdleState => _idleState;
        MonsterMovingState _movingState;
        public MonsterMovingState MovingState => _movingState;
        GameObject _target;
        public GameObject Target { get { return _target; } 
            set { _target = value; } }

        public GameObject FindNearestTarget()
        {
            GameObject nearestObj = null;

            GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerObject");

            if (objs.Length == 0)
                return null;

            float nearest = Mathf.Infinity;
            for (int i = 0; i < objs.Length; i++)
            {
                Vector3 dist = objs[i].gameObject.transform.position - gameObject.transform.position;
                if (nearest > dist.sqrMagnitude)
                {
                    nearest = dist.sqrMagnitude;
                    nearestObj = objs[i];
                }
            }

            return nearestObj;
        }

        public void Move()
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 5.0f, Space.Self);
        }

        public void Look(GameObject target)
        {
            gameObject.transform.LookAt(target.transform);
        }

        public override void Start()
        {
            base.Start();
            _target = null;
            _idleState = new MonsterIdleState(this);
            _movingState = new MonsterMovingState(this);
            base.StateMachine.Initialize(_idleState);
        }

        public override void Update()
        {
            base.Update();

        }
    }

}
