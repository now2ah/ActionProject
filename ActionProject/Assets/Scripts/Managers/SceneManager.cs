using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;

namespace Action.Manager
{
    public class SceneManager : Singleton<SceneManager>
    {
        int _sceneNumToLoad;
        public int SceneNumToLoad => _sceneNumToLoad;

        GameObject _fadeUI;
        float _fadeSpeed;

        public override void Initialize()
        {
            base.Initialize();
            _sceneNumToLoad = 0;
            _LoadFadeImage();
            _fadeSpeed = 0.5f;
            //UnityEngine.SceneManagement.SceneManager.sceneLoaded += _OnSceneLoaded;
        }

        public void LoadGameScene(int sceneNumber, LoadSceneMode mode = LoadSceneMode.Single)
        {
            _sceneNumToLoad = sceneNumber;
            UnityEngine.SceneManagement.SceneManager.LoadScene("99.Loading", mode);
        }

        void _OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            
        }

        #region FADE IN AND OUT
        public void Fade(UIManager.eFade fade, UnityAction action = null)
        {
            UIManager.Instance.Fade(fade, UIManager.Instance.FadeUIPanel, _fadeSpeed, action);
        }

        void _LoadFadeImage()
        {
            //_fadeUI = UIManager.Instance.CreateUI("FadeInNOutPanel", UIManager.Instance.MainCanvas);
            //_fadeUI.SetActive(false);
        }
        #endregion


    }
}
