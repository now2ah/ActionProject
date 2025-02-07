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
        protected abstract List<UI.UI> _GetAllUIs(List<GameObject> gameObjects);

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
    }

}