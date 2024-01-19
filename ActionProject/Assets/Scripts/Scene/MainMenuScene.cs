using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;

namespace Action.Scene
{
    public class MainMenuScene : MonoBehaviour
    {
        GameObject _mainMenuPanel;
        Button _startButton;

        void _LoadNextScene()
        {
            SceneManager.Instance.LoadGameScene(2);
        }

        void Start()
        {
            _mainMenuPanel = UIManager.Instance.CreateUI("MainMenuPanel", UIManager.Instance.MainCanvas);
            _startButton = _mainMenuPanel.transform.GetComponentInChildren<Button>();
            _startButton.onClick.AddListener(() =>
            {
                _LoadNextScene();
            });
        }

        private void OnDestroy()
        {
            Destroy(_mainMenuPanel);
        }
    }
}