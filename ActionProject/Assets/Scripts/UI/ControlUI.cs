using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;


namespace Action.UI
{
    public class ControlUI : InGameTargetUI
    {
        protected bool _isChild = false;

        public override void SetParent(Transform tr)
        {
            transform.SetParent(tr);
            transform.localPosition = Vector3.zero;
            _isChild = true;
        }

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (!_isChild)
                _FollowTargetPosition(ePanelPosition.TOP);
        }
    }
}
