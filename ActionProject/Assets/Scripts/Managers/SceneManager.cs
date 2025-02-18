using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;
using Action.Scene;
using Cysharp.Threading.Tasks;
using Action.UI;
using System.Threading.Tasks;

namespace Action.Manager
{
    public class SceneManager : Singleton<SceneManager>
    {
        int _sceneNumToLoad;
        SceneObject _currentSceneObj;

        float _fadeSpeed;

        public int SceneNumToLoad => _sceneNumToLoad;
        public SceneObject CurrentSceneObj { get { return _currentSceneObj; } set { _currentSceneObj = value; } }

        #region SCENE_LOADED_EVENT
        
        public UnityEvent onIntroSceneLoaded;
        public UnityEvent onLoadingSceneLoaded;
        //... so on

        #endregion

        public override void Initialize()
        {
            _sceneNumToLoad = 0;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

            _fadeSpeed = 1f;
        }

        public void LoadGameScene(Enums.eScene scene)
        {
            _sceneNumToLoad = (int)scene;
            UnityEngine.SceneManagement.SceneManager.LoadScene("99.Loading", LoadSceneMode.Additive);
        }

        public async UniTask<AsyncOperation> LoadGameSceneAsync(Enums.eScene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((int)scene);
            await op;
            return op;
        }
        
        public void LoadNextScene()
        {
            if (_sceneNumToLoad < UnityEngine.SceneManagement.SceneManager.sceneCount) { _sceneNumToLoad++; }

            LoadGameScene((Enums.eScene)_sceneNumToLoad);
        }

        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            switch (scene.buildIndex)
            {
                case (int)Enums.eScene.INTRO:
                    onIntroSceneLoaded.Invoke();
                    break;
                case (int)Enums.eScene.LOADING:
                    onLoadingSceneLoaded.Invoke();
                    break;
            }
            //... so on
        }

        #region FADE_IN_AND_OUT

        public async UniTask Fade(UIManager.eFade fade, UnityAction action = null)
        {
            await UIManager.Instance.Fade(fade, _fadeSpeed);
        }

        #endregion
    }
}
