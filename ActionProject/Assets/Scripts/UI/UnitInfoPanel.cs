using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using Action.Units;

namespace Action.UI
{
    public class UnitInfoPanel : InGameUI
    {
        GameObject _target;
        Unit _unit;
        Image _fillimage;
        TextMesh _nameText;
        float _panelHeight;
        public void Initialize(GameObject target, string name = "defalut")
        {
            _target = target;
            _fillimage = transform.GetChild(0).transform.GetComponent<Image>();
            _fillimage.type = Image.Type.Filled;
            _nameText = transform.GetChild(1).transform.GetComponent<TextMesh>();
            _nameText.text = name;
            _panelHeight = base.rectTr.rect.height;
            ApplyHPValue(_unit.HP); //default hp
        }

        public void SetName(string name)
        {
            _nameText.text = name;
        }

        public void SetTextStyle(int size, Color color)
        {
            _nameText.fontSize = size;
            _nameText.color = color;
        }

        public void ApplyHPValue(float hp)
        {
            _fillimage.fillAmount = Mathf.Clamp01(hp / 0.9f);
        }

        void _FollowTargetPosition()
        {
            if (null != _target)
            {
                transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(_GetBottomPosition()) - new Vector3(0.0f, _panelHeight, 0.0f); ;
            }
        }

        Vector3 _GetBottomPosition()
        {
            Collider col = _target.GetComponentInChildren<Collider>();
            Vector3 panelPos = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.min.z);

            return panelPos;
        }

        private void Update()
        {
            _FollowTargetPosition();
        }
    }

}