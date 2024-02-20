using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using Action.Units;
using TMPro;

namespace Action.UI
{
    public class UnitInfoPanel : InGameTargetUI
    {
        Unit _unit;
        Image _fillimage;
        string _nameText;
        TextMeshProUGUI _textMesh;
        
        bool _isVisible = true;

        public override void Initialize(GameObject target, string name = "defalut")
        {
            base.Initialize(target, name);
            _unit = target.GetComponent<Unit>();
            _fillimage = transform.GetChild(0).transform.GetComponent<Image>();
            _fillimage.type = Image.Type.Filled;
            _textMesh = transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
            _textMesh.text = name;
            
            ApplyHPValue(_unit.HP, _unit.MaxHp); //default hp
        }

        public void SetVisible(bool isOn)
        {
            if (_fillimage.enabled != isOn && _textMesh.enabled != isOn)
            {
                _fillimage.enabled = isOn;
                _textMesh.enabled = isOn;
                _isVisible = isOn;
            }
        }

        public void ApplyHPValue(float hp, float fullHP)
        {
            _fillimage.fillAmount = Mathf.Clamp01(hp / fullHP);
        }

        void _CheckVisibleDistant()
        {
            if (Vector3.Distance(GameManager.Instance.PlayerUnitObj.transform.position, _target.transform.position) <
                Constant.INGAMEUI_VISIBLE_DISTANT)
                SetVisible(true);
            else
                SetVisible(false);
        }


        protected override void Update()
        {
            _CheckVisibleDistant();
            if (_isVisible)
                _FollowTargetPosition(ePanelPosition.BOTTOM);
        }
    }

}