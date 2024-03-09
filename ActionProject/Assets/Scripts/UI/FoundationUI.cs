using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class FoundationUI : ControlUI
    {
        protected GameObject _buildPanel;
        public GameObject BuildPanel { get { return _buildPanel; } }

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
        }
        protected override void Awake()
        {
            base.Awake();
            _buildPanel = transform.GetChild(0).gameObject;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            //_FollowTargetPosition(ePanelPosition.TOP);
        }
    }
}
