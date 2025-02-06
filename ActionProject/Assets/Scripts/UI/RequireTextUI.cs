using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Action.UI
{
    public class RequireTextUI : UI
    {
        TextMeshProUGUI _text;
        public TextMeshProUGUI Text { get { return _text; } set { _text = value; } }

        public override void Initialize()
        {
            
            transform.localPosition = transform.localPosition + new Vector3(0, 1.0f, 0);
        }

        protected override void Awake()
        {
            base.Awake();
            _text = transform.GetComponentInChildren<TextMeshProUGUI>();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}

