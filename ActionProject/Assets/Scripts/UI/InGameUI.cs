using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class InGameUI : UI
    {
        protected RectTransform rectTr;

        public void ApplyRect(float width, float height)
        {
            if (null != rectTr)
            {
                rectTr.sizeDelta = new Vector2(width, height);
            }
        }

        private void Awake()
        {
            rectTr = transform.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
