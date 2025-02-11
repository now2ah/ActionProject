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

        //change return type with pair <string, Enums.eScene>
        protected override List<GameObject> _LoadUIAssets()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            gameObjects.Add(AssetManager.Instance.LoadAsset(eAssetType.UI, "IntroPanel"));

            return gameObjects;
        }

        private void Awake()
        {
            Initialize();
            ShowUIObjects(true);
        }
    }
}