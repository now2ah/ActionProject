using Action.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Scene
{
    public abstract class SceneObject : MonoBehaviour
    {
        List<UI.UI> _uiList;
        public List<UI.UI> UIList => _uiList;

        public virtual void Initialize()
        {
            _uiList = new List<UI.UI>();
        }

        /// <summary>
        /// Load UI Assets that Scene needed
        /// </summary>
        /// <returns></returns>
        protected abstract List<GameObject> _LoadUIAssets();

        protected void _AddToUIList(List<GameObject> uiAssets)
        {
            foreach (GameObject uiAsset in uiAssets)
            {
                if (uiAsset.TryGetComponent<UI.UI>(out UI.UI ui))
                {
                    _uiList.Add(ui);
                }
            }
        }

        protected void _AddToOwnCanvas(List<UI.UI> uiList)
        {
            if (null != uiList)
            {
                foreach(var ui in uiList)
                {
                    ui.AddToCanvas();
                }
            }
        }

        public void ShowUIObjects(bool isOn)
        {
            if (null != _uiList)
            {
                foreach(UI.UI ui in _uiList)
                {
                    if (isOn)
                    { ui.Show(); }
                    else if (!isOn)
                    { ui.Hide(); }
                }
            }
        }
    }

}