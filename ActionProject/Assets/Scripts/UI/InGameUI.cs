using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class InGameUI : UI
    {
        RectTransform rectTr;

        public void ApplyRect(float width, float height)
        {
            if (null != rectTr)
            {
                rectTr.sizeDelta = new Vector2(width, height);
                //rectTr.rect.Set(width, height);
            }
        }

        void _FollowUnit()
        {
            if (null != rectTr)
                rectTr.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(transform.position);
        }

        private void Start()
        {
            rectTr = transform.GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            //_FollowUnit();
        }
    }
}
