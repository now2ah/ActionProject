using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class ControlUI : InGameTargetUI
    {
        protected GameObject _buildPanel;
        public GameObject BuildPanel { get { return _buildPanel; } }
        protected GameObject _controlPanel;
        public GameObject ControlPanel { get { return _controlPanel; } }
        
        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
        }

        protected override void Awake()
        {
            base.Awake();
            _buildPanel = transform.GetChild(0).gameObject;
            _controlPanel = transform.GetChild(1).gameObject;
        }

        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}
