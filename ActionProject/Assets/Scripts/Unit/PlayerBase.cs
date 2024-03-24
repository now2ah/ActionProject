using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class PlayerBase : Building
    {
        public override void Initialize()
        {
            base.Initialize();
            _constructTime = 0.0f;
            StateMachine.ChangeState(_prepareState);
            StateMachine.ChangeState(_doneState);
        }

        protected override void Start()
        {
            MaxHp = 1000;
            HP = MaxHp;
            name = "Base";
            base.Start();
            
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
