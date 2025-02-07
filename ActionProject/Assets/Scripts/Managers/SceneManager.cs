using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;

namespace Action.Manager
{
    
    public class SceneUIList
    {
        List<UI.UI> _uiList;

        public void Initialize()
        {
            List<UI.UI> _ui = new List<UI.UI>();
        }

        public void AddUI(UI.UI uiObj)
        {
            if (null == uiObj) { Logger.LogError("UI is null"); }

            if (null != _uiList)
            {
                _uiList.Add(uiObj);
            }
        }
    }

    public class SceneManager : Singleton<SceneManager>
    {
        int _sceneNumToLoad;

        public int SceneNumToLoad => _sceneNumToLoad;

        #region SCENE_LOADED_EVENT
        
        public UnityEvent onIntroSceneLoaded;

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

        //working on...
        //public void LoadNextScene()
        //{
        //    _sceneNumToLoad++;
        //}

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
            UIManager.Instance.Fade(fade, UIManager.Instance.FadeUIPanel, _fadeSpeed, action);
        }

        #endregion
    }
}
