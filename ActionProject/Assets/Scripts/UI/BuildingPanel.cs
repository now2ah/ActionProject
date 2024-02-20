using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class BuildingPanel : ControlUI
    {
        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            _FollowTargetPosition(ePanelPosition.TOP);
        }
    }
}
