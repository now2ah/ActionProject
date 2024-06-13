using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Action.Util;

namespace Action.UI
{
    public class TimerUI : InGameTargetUI
    {
        TextMeshProUGUI _text;
        ActionTime _actionTime;
        public ActionTime ActionTime { get { return _actionTime; } set { _actionTime = value; } }

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
            _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _actionTime = null;
        }

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            if (null != _actionTime)
            {
                _text.text = _actionTime.GetTimeString();
                if (_actionTime.IsFinished)
                    Destroy(gameObject);
            }
        }

        protected void FixedUpdate()
        {
            _FollowTargetPosition(ePanelPosition.CENTER);
        }
    }

}