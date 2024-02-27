using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Manager;

namespace Action.UI
{
    public class HouseUI : BuildingUI
    {
        House _house;
        
        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
            _house = _target.GetComponent<House>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}