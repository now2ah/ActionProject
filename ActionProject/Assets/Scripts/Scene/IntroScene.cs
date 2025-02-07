using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Manager;

namespace Action.Scene
{
    public class IntroScene : SceneObject
    {
        public override void Initialize()
        {
            base.Initialize();
            AddUIObjects(_GetAllUIs(_LoadUIAssets()));
        }

        protected override List<GameObject> _LoadUIAssets()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            gameObjects.Add(AssetManager.Instance.LoadAsset(eAssetType.UI, "IntroPanel"));

            return gameObjects;
        }

        protected override List<UI.UI> _GetAllUIs(List<GameObject> gameObjects)
        {
            List<UI.UI> uiList = new List<UI.UI>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (TryGetComponent<UI.UI>(out  UI.UI ui))
                {
                    uiList.Add(ui);
                }
            }
            return uiList;
        }

        private void Awake()
        {
            Initialize();
        }
    }
}