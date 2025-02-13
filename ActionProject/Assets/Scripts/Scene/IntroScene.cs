using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Manager;
using System;

namespace Action.Scene
{
    public class IntroScene : SceneObject
    {
        List<GameObject> _uiAssets;

        public override void Initialize()
        {
            base.Initialize();
            _uiAssets = new List<GameObject>();
            _uiAssets = _LoadUIAssets();
            _AddToUIList(_uiAssets);
            _AddToOwnCanvas(UIList);
        }

        protected override List<GameObject> _LoadUIAssets()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            gameObjects.Add(AssetManager.Instance.LoadAsset(eAssetType.UI, "IntroPanel"));

            return gameObjects;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            ShowUIObjects(true);
        }
    }
}