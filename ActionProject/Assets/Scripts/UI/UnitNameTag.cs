using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;

namespace Action.UI
{
    public class UnitNameTag : InGameUI
    {
        Text _nameText;

        public void Initialize(string name = "defalut")
        {
            _nameText = transform.GetChild(0).GetComponent<Text>();
            _nameText.text = name;
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

        void _LookAtCamera()
        {
            transform.LookAt(CameraManager.Instance.MainCamera.Camera.transform);
        }

        private void Update()
        {
            _LookAtCamera();
        }
    }

}