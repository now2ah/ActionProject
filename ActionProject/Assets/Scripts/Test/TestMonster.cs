using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;


public class TestMonster : MonoBehaviour
{
    StateMachine _stateMachine;
    IdleState _idleState;
    MovingState _movingState;

    // Start is called before the first frame update
    void Start()
    {
        _idleState = new IdleState();
        _movingState = new MovingState();
        _stateMachine = gameObject.AddComponent<StateMachine>();
        _stateMachine.Initialize();
        _stateMachine.ChangeState(_idleState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
