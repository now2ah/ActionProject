using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class ControlUI : InGameUI
    {
        protected GameObject _owner;
        protected GameObject _buildPanel;
        public GameObject BuildPanel { get { return _buildPanel; } }
        protected GameObject _controlPanel;
        public GameObject ControlPanel { get { return _controlPanel; } }
        
        protected void _FollowTargetPosition()
        {
            if (null != _owner)
            {
                transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(_owner.transform.position);
            }
        }

        private void Awake()
        {
            _buildPanel = transform.GetChild(0).gameObject;
            _controlPanel = transform.GetChild(1).gameObject;
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
