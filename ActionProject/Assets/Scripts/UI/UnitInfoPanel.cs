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
        Text _nameText;
        float _panelHeight;
        bool _isVisible = true;

        public void Initialize(GameObject target, string name = "defalut")
        {
            _target = target;
            _unit = target.GetComponent<Unit>();
            _fillimage = transform.GetChild(0).transform.GetComponent<Image>();
            _fillimage.type = Image.Type.Filled;
            _nameText = transform.GetChild(1).transform.GetComponent<Text>();
            _nameText.text = name;
            _panelHeight = base.rectTr.rect.height;
            ApplyHPValue(_unit.HP, _unit.FullHp); //default hp
        }

        public void SetVisible(bool isOn)
        {
            if (_fillimage.enabled != isOn && _nameText.enabled != isOn)
            {
                _fillimage.enabled = isOn;
                _nameText.enabled = isOn;
                _isVisible = isOn;
            }
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

        public void ApplyHPValue(float hp, float fullHP)
        {
            _fillimage.fillAmount = Mathf.Clamp01(hp / fullHP);
        }

        void _CheckVisibleDistant()
        {
            if (Vector3.Distance(GameManager.Instance.PlayerUnit.transform.position, _target.transform.position) <
                Constant.INGAMEUI_VISIBLE_DISTANT)
                SetVisible(true);
            else
                SetVisible(false);
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
            _CheckVisibleDistant();
            if (_isVisible)
                _FollowTargetPosition();
        }
    }

}