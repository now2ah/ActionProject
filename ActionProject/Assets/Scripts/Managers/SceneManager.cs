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

namespace Action.Manager
{
    public class SceneManager : Singleton<SceneManager>
    {
        int _sceneNumToLoad;
        SceneObject _currentSceneObj;

        public int SceneNumToLoad => _sceneNumToLoad;
        public SceneObject CurrentSceneObj { get { return _currentSceneObj; } set { _currentSceneObj = value; } }

        #region SCENE_LOADED_EVENT
        
        public UnityEvent onIntroSceneLoaded;
        //... so on

        #endregion

        public override void Initialize()
        {
            _sceneNumToLoad = 0;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

            _fadeSpeed = 0.5f;
        }

        public void LoadGameScene(Enums.eScene scene, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _sceneNumToLoad = (int)scene;
            UnityEngine.SceneManagement.SceneManager.LoadScene("99.Loading", mode);
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
            }
            //... so on
        }

        #region FADE_IN_AND_OUT

        float _fadeSpeed;

        public void Fade(UIManager.eFade fade, UnityAction action = null)
        {
            UIManager.Instance.Fade(fade, (FadePanelUI)UIManager.Instance.GetMiscUI("FadePanelUI"), _fadeSpeed, action);
        }

        #endregion
    }
}
