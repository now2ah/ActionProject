using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Action.State;

public class TestPlayer : MonoBehaviour
{
    Vector2 inputVector;
    StateMachine _stateMachine;
    IdleState _idleState;
    MovingState _movingState;

    void OnTestAction(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_movingState);
        inputVector = context.ReadValue<Vector2>();
    }

    void OnTestActionCanceled(InputAction.CallbackContext context)
    {
        _stateMachine.ChangeState(_idleState);
        inputVector = Vector3.zero;
    }

    public void Move()
    {
        Vector3 movePos = new Vector3(inputVector.x, 0, inputVector.y);
        
        if(_stateMachine.IsState(_movingState))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos), 0.15f);

        transform.Translate(movePos * Time.deltaTime * 5.0f, Space.World);
    }

    // Start is called before the first frame update
    void Start()
    {
        Action.Manager.InputManager.Instance.actionMove.performed += ctx => { OnTestAction(ctx); };
        Action.Manager.InputManager.Instance.actionMove.canceled += ctx => { OnTestActionCanceled(ctx); };
        _idleState = new IdleState();
        _movingState = new MovingState();
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(_idleState);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.Update();
    }
}
