using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.UI
{
    public enum ePanelPosition
    {
        CENTER,
        TOP,
        BOTTOM
    }

    public class InGameTargetUI : InGameUI
    {
        protected GameObject _target;
        protected float _panelHeight;
        protected Vector3 _offset;

        public virtual void Initialize(GameObject target, string name = "default")
        {
            _target = target;
            _panelHeight = base.rectTr.rect.height;
            _offset = Vector3.zero;
        }

        protected void _FollowTargetPosition(ePanelPosition pos)
        {
            if (null != _target)
            {
                switch (pos)
                {
                    case ePanelPosition.TOP:
                        _offset.y = _panelHeight;
                        break;
                    case ePanelPosition.CENTER:
                        
                        break;
                    case ePanelPosition.BOTTOM:
                        _offset.y = _panelHeight * -2.0f;
                        break;
                }
                transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(_GetColliderPosition(pos)) + _offset;
            }
        }

        protected Vector3 _GetColliderPosition(ePanelPosition pos)
        {
            Collider col = _target.GetComponentInChildren<Collider>();
            Vector3 panelPos = Vector3.zero;

            switch (pos)
            {
                case ePanelPosition.TOP:
                    panelPos = new Vector3(col.bounds.center.x, col.bounds.max.y, col.bounds.max.z);
                    break;

                case ePanelPosition.CENTER:
                    panelPos = new Vector3(col.bounds.center.x, col.bounds.center.y, col.bounds.min.z);
                    break;

                case ePanelPosition.BOTTOM:
                    panelPos = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.min.z);
                    break;
            }
            return panelPos;
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
        }
    }
}
