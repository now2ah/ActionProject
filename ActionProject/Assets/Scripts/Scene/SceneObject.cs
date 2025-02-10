using Action.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Scene
{
    public abstract class SceneObject : MonoBehaviour
    {
        SceneUIList _uiList;

        public virtual void Initialize()
        {
            _uiList = new SceneUIList();
            _uiList.Initialize();
        }

        protected abstract List<GameObject> _LoadUIAssets();

        protected List<UI.UI> _GetAllUIs(List<GameObject> gameObjects)
        {
            List<UI.UI> uiList = new List<UI.UI>();

            if (null != gameObjects && gameObjects.Count > 0)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    if (TryGetComponent<UI.UI>(out UI.UI ui))
                    {
                        uiList.Add(ui);
                    }
                }
            }
            else
            {
                Logger.LogError("can't load scene UI Assets");
            }

            return uiList;
        }

        public void AddUIObjects(List<UI.UI> uiList)
        {
            if (null != _uiList)
            {
                foreach (UI.UI ui in uiList)
                {
                    _uiList.AddUI(ui);
                }
            }
        }


        //working on... shoud UIList be public??
        //public void ShowUIObjects(bool isOn)
        //{
        //    if (null != _uiList && _uiList.GetUICount() > 0)
        //    {
        //        for (int i=0; i< _uiList.GetUICount(); i++)
        //        {
        //            _uiList[i].ShowUIObjects(isOn);
        //        }
        //    }
        //}
    }

}