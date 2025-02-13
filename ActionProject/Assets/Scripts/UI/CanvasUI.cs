using Action.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class CanvasUI : UI
    {
        #region private
        Canvas _canvas;
        List<UI> _uiList;
        #endregion

        public Canvas Canvas { get { return _canvas; } set { _canvas = value; } }
        public List<UI> UIList => _uiList;

        public override void Initialize()
        {
            if (null == _canvas) { _canvas = transform.GetComponent<Canvas>(); } 
            if (null == _uiList) { _uiList = new List<UI>(); }

            _canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            
            if (TryGetComponent<CanvasScaler>(out CanvasScaler scaler))
            {
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            }
        }

        public void AddUI()
        {

        }
    }

}