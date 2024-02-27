using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class BuildingUI : ControlUI
    {
        protected GameObject _buildPanel;
        public GameObject BuildPanel { get { return _buildPanel; } }
        protected GameObject _usePanel;
        public GameObject UsePanel { get { return _usePanel; } }

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
        }
        protected override void Awake()
        {
            base.Awake();
            _buildPanel = transform.GetChild(0).gameObject;
            _usePanel = transform.GetChild(1).gameObject;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            //_FollowTargetPosition(ePanelPosition.TOP);
        }
    }
}
