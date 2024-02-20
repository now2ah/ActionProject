using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;

namespace Action.Units
{
    public class PlayerBuilding : Building
    {

        protected override void Start()
        {
            MaxHp = 1000;
            HP = MaxHp;
            base.Start();
            
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
