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

        public override void Initialize()
        {
            base.Initialize();
            //base.SetName("UIManager");

            _CreateMainCanvas();
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

        public void SetUIIndex(int index)
        {

        }

        void _CreateMainCanvas()
        {
            _mainCanvasObject = Instantiate(Resources.Load("Prefabs/UI/MainCanvasObject") as GameObject);
            _mainCanvasObject.transform.SetParent(this.transform, false);
            _mainCanvas = _mainCanvasObject.GetComponentInChildren<Canvas>();
        }
    }
}