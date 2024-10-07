using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using TMPro;

namespace Action.Scene
{
    public class MainMenuScene : MonoBehaviour
    {
        GameObject _mainMenuPanel;
        TextMeshProUGUI _titleText;
        Button _continueButton;
        Button _startButton;

        void _LoadNextScene()
        {
            SceneManager.Instance.LoadGameScene(2);
        }

        void Start()
        {
            CameraManager.Instance.CreateMainMenuVirtualCamera();

            _mainMenuPanel = UIManager.Instance.CreateUI("MainMenuPanel", UIManager.Instance.MainCanvas);
            _titleText = _mainMenuPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            SaveSystem.Instance.LoadSourceData();

            _continueButton = _mainMenuPanel.transform.GetChild(1).GetComponent<Button>();
            _continueButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SaveLoadUI.Initialize(UI.SaveLoadUI.eMode.LOAD);
                UIManager.Instance.SaveLoadUI.Show();
                UIManager.Instance.SaveLoadPanel.transform.SetAsLastSibling();
            });
            if (SaveSystem.Instance.HasSaveData())
                _continueButton.gameObject.SetActive(true);
            else
                _continueButton.gameObject.SetActive(false);
            _startButton = _mainMenuPanel.transform.GetChild(2).GetComponent<Button>();
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