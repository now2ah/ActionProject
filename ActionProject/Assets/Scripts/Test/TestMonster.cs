using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;


public class TestMonster : MonoBehaviour
{
    StateMachine _stateMachine;
    MonsterIdleState _idleState;
    //MonsterMovingState _movingState;
    //GameObject _target;


    // Start is called before the first frame update
    void Start()
    {
        //_idleState = new MonsterIdleState(this);
        //_movingState = new MonsterMovingState(this);
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(_idleState);
        //_target = null;
    }

    // Update is called once per frame
    void Update()
    {
        //if (null == _target)
        //_target = _FindNearestTarget();
        _stateMachine.Update();
    }
}
