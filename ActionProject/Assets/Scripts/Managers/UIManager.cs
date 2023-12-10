using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Util;

namespace Action.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        GameObject _mainCanvasObject;
        Canvas _mainCanvas;
        public Canvas MainCanvas => _mainCanvas;
        bool _isOnOffScreenIndicator;
        GameObject _OffScreenIndicator;

        public override void Initialize()
        {
            base.Initialize();
            //base.SetName("UIManager");

            _CreateMainCanvas();
            _isOnOffScreenIndicator = false;
            _OffScreenIndicator = CreateUI("OffScreenIndicator");
            _OffScreenIndicator.SetActive(false);
        }

        public GameObject CreateUI(string name)
        {
            string uiPath = "Prefabs/UI/";
            GameObject obj = Instantiate(Resources.Load(uiPath + name) as GameObject);

            if (null == obj)
                Debug.LogError("UI Prefab is missing.");

            if (null != _mainCanvas)
                obj.transform.SetParent(_mainCanvas.transform, false);

            return obj;
        }

        public void TurnOffScreenIndicator(bool isOn)
        {
            _OffScreenIndicator.SetActive(true);
            _isOnOffScreenIndicator = isOn;
        }

        void _CreateMainCanvas()
        {
            _mainCanvasObject = Instantiate(Resources.Load("Prefabs/UI/MainCanvasObject") as GameObject);
            _mainCanvasObject.transform.SetParent(this.transform, false);
            _mainCanvas = _mainCanvasObject.GetComponentInChildren<Canvas>();
        }

        void _CalculateOffScreenIndicator()
        {
            if (null == _OffScreenIndicator)
                return;

            Vector3 screenPos = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(_OffScreenIndicator.transform.position);

            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                TurnOffScreenIndicator(true);

                //float angle = Mathf.Atan2(screenPos.y - 
            }
        }

        private void Update()
        {
            _CalculateOffScreenIndicator();
        }
    }
}