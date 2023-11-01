using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

public class IdleState : State
{
    public override void EnterState()
    {
        Debug.Log("Enter Idle");
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle");
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
}
