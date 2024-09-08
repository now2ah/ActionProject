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
            if (_isActive)
                return;

            base.Initialize();

            StateMachine.ChangeState(_prepareState);
            StateMachine.ChangeState(_doneState);
        }

        protected override void Awake()
        {
            base.Awake();
            _constructTime = 0.0f;
            MaxHp = 1000;
            HP = MaxHp;
            UnitName = "Base";
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
