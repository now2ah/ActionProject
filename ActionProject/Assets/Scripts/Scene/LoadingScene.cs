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
        List<GameObject> _uiAssets;

        GameObject _loadingPanel;
        Image _fillImage;

        public override void Initialize()
        {
            base.Initialize();
            _uiAssets = new List<GameObject>();
            _uiAssets = _LoadUIAssets();
            _AddToUIList(_uiAssets);
            _AddToOwnCanvas(UIList);

            if (null != _loadingPanel)
            {
                _fillImage = _loadingPanel.transform.GetChild(1).GetChild(0).GetComponent<Image>();
            }
        }

        protected override List<GameObject> _LoadUIAssets()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            _loadingPanel = AssetManager.Instance.LoadAsset(eAssetType.UI, "LoadingPanel");

            gameObjects.Add(_loadingPanel);

            return gameObjects;
        }

        async UniTask<AsyncOperation> LoadGameSceneAsync(Enums.eScene scene)
        {
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)scene);
            while (op.progress < 0.9f)
            {
                float progressValue = Mathf.Clamp01(op.progress / 0.9f);

                _fillImage.fillAmount = progressValue;
            }
            await op;
            return op;
        }

        private void Awake()
        {
            Initialize();
        }

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