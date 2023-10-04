using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Util;

namespace Action.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        GameObject mainCanvasObject;
        Canvas mainCanvas;

        public override void Initialize()
        {
            base.Initialize();
            base.SetName("UIManager");

            _CreateMainCanvas();
        }

        public GameObject CreateUI(string name)
        {
            GameObject obj = Instantiate(Resources.Load(name) as GameObject);

            if (null == obj)
                Debug.LogError("UI Prefab is missing.");

            if (null != mainCanvas)
                obj.transform.SetParent(mainCanvas.transform, false);

            return obj;
        }

        void _CreateMainCanvas()
        {
            mainCanvasObject = Instantiate(Resources.Load("Prefabs/UI/MainCanvasObject") as GameObject);
            mainCanvasObject.transform.SetParent(this.transform, false);
            mainCanvas = mainCanvasObject.GetComponentInChildren<Canvas>();
        }
    }
}