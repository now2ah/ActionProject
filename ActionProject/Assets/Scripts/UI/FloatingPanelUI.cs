using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Action.UI
{
    public class FloatingPanelUI : InGameTargetUI
    {
        TextMeshProUGUI _text;
        public TextMeshProUGUI Text { get { return _text; } set { _text = value; } }

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
            _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(DestroyCoroutine());
        }

        protected override void Update()
        {
            base.Update();
            transform.position = transform.position + Vector3.up;
        }

        protected void FixedUpdate()
        {
            _FollowTargetPosition(ePanelPosition.CENTER);
        }
    }
}

