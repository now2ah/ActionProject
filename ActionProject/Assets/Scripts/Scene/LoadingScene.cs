using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using Cysharp.Threading.Tasks;

namespace Action.Scene
{
    public class LoadingScene : SceneObject
    {
        GameObject _loadingPanel;
        Image _fillImage;

        public override void Initialize()
        {
            base.Initialize();
            AddUIObjects(_GetAllUIs(_LoadUIAssets()));
        }

        protected override List<GameObject> _LoadUIAssets()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            _loadingPanel = AssetManager.Instance.LoadAsset(eAssetType.UI, "LoadingPanel");
            _fillImage = _loadingPanel.transform.GetChild(1).GetChild(0).GetComponent<Image>();

            gameObjects.Add(_loadingPanel);

            return gameObjects;
        }

        async UniTask<AsyncOperation> LoadGameSceneAsync(Enums.eScene scene)
        {
            UniTask<AsyncOperation> task = SceneManager.Instance.LoadGameSceneAsync(scene);

            ValueTask<AsyncOperation> valueTask = task.AsValueTask();

            AsyncOperation op = valueTask.Result;

            while (!op.isDone)
            {
                float progressValue = Mathf.Clamp01(op.progress / 0.9f);

                _fillImage.fillAmount = progressValue;
            }
            await task;
            return op;
        }

        // Start is called before the first frame update
        async void Start()
        {
            await LoadGameSceneAsync((Enums.eScene)SceneManager.Instance.SceneNumToLoad);
        }

        private void OnDestroy()
        {
            Destroy(_loadingPanel);
        }
    }
}