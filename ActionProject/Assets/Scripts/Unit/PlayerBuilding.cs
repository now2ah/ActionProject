using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class PlayerBuilding : Unit
    {
        PlayerBuildingIdleState _idleState;

        public override void Start()
        {
            base.Start();
            _idleState = new PlayerBuildingIdleState(this);
            base.StateMachine.Initialize(_idleState);
        }

        public override void Update()
        {
            base.Update();
        }
    }

}
