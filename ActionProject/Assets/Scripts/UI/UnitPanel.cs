using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using Action.Units;
using TMPro;

namespace Action.UI
{
    public class UnitPanel : InGameTargetUI
    {
        Unit _unit;
        Image _fillimage;
        TextMeshProUGUI _textMesh;
        ePanelPosition _panelPosition;

        public ePanelPosition PanelPosition { get { return _panelPosition; } set { _panelPosition = value; } }
        
        bool _isVisible = true;

        public override void Initialize(GameObject target, string name = "default")
        {
            base.Initialize(target, name);
            _unit = target.GetComponent<Unit>();
            _fillimage = transform.GetChild(0).transform.GetComponent<Image>();
            _fillimage.type = Image.Type.Filled;
            _textMesh = transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>();
            _textMesh.text = _unit.UnitData.name;
            _panelPosition = ePanelPosition.BOTTOM;
        }

        public void SetNameText(string name)
        {
            _textMesh.text = name;
        }

        public void ApplyHPValue(float hp, float fullHP)
        {
            _fillimage.fillAmount = Mathf.Clamp01(hp / fullHP);
        }

        protected override void Update()
        {
            if (_isVisible)
                _FollowTargetPosition(_panelPosition);
        }

        private void FixedUpdate()
        {
            
        }
    }

}