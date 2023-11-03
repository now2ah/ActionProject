using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;


public class TestMonster : MonoBehaviour
{
    StateMachine _stateMachine;
    MonsterIdleState _idleState;
    MonsterMovingState _movingState;
    GameObject _target;

    GameObject _FindNearestTarget()
    {
        GameObject nearestObj = null;
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PlayerObject");

        if (objs.Length == 0)
            return null;

        float nearest = Mathf.Infinity;
        for(int i = 0; i < objs.Length; i++)
        {
            Vector3 dist = objs[i].gameObject.transform.position - gameObject.transform.position;
            if(nearest > dist.sqrMagnitude)
            {
                nearest = dist.sqrMagnitude;
                nearestObj = objs[i];
            }
        }

        return nearestObj;
    }

    // Start is called before the first frame update
    void Start()
    {
        _idleState = new MonsterIdleState();
        _movingState = new MonsterMovingState();
        _stateMachine = gameObject.AddComponent<StateMachine>();
        _stateMachine.Initialize();
        _stateMachine.ChangeState(_idleState);
        _target = null;
        if(null != _target)
            Debug.Log(_FindNearestTarget().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (null == _target)
            _target = _FindNearestTarget();
    }
}
