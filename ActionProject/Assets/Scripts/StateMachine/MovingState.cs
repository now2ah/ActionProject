using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

public class MovingState : State
{
    public override void EnterState()
    {
        Debug.Log("Enter Moving");
    }

    public override void ExitState()
    {
        Debug.Log("Exit Moving");
    }

    // Update is called once per frame
    public override void Update()
    {

    }
}
