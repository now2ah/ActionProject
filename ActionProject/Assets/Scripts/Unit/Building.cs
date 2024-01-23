using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class Building : Unit
    {
        PlayerBuildingIdleState _idleState;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _idleState = new PlayerBuildingIdleState(this);
            base.StateMachine.Initialize(_idleState);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }

}