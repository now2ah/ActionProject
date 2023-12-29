using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;

namespace Action.UI
{
    public class UnitInfoPanel : InGameUI
    {
        GameObject _target;
        Image _fillimage;
        TextMesh _nameText;

        public void Initialize(GameObject target, string name = "defalut")
        {
            _target = target;
            _fillimage = transform.GetChild(0).transform.GetComponent<Image>();
            _fillimage.type = Image.Type.Filled;
            _nameText = transform.GetChild(1).transform.GetComponent<TextMesh>();
            _nameText.text = name;
            ApplyHPValue(100f); //default hp
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
                transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(_target.transform.position);
        }

        private void Update()
        {
            _FollowTargetPosition();
        }
    }

}