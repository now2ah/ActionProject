using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class InGameUI : UI
    {
        protected RectTransform rectTr;

        public override void Initialize()
        {
            
        }

        public void ApplyRect(float width, float height)
        {
            if (null != rectTr)
            {
                rectTr.sizeDelta = new Vector2(width, height);
            }
        }

        protected override void Awake()
        {
            base.Awake();
            rectTr = transform.GetComponent<RectTransform>();
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
