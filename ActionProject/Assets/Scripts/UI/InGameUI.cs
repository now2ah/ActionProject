using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public class InGameUI : UI
    {
        RectTransform rectTr;

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
            _FollowUnit();
        }
    }
}
