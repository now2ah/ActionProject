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
        Button _optionButton;
        Button _exitGameButton;

        void _LoadNextScene()
        {
            //SceneManager.Instance.LoadGameScene(Enums.eScene.INGAME);
        }

        void Start()
        {
            CameraManager.Instance.CreateMainMenuVirtualCamera();

            _mainMenuPanel = UIManager.Instance.CreateUI("MainMenuPanel", UIManager.Instance.MainCanvas.Canvas);
            _titleText = _mainMenuPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            SaveSystem.Instance.LoadSourceData();

            _continueButton = _mainMenuPanel.transform.GetChild(1).GetChild(0).GetComponent<Button>();
            _continueButton.onClick.AddListener(() =>
            {
                UIManager.Instance.SaveLoadUI.Initialize(UI.SaveLoadUI.eMode.LOAD);
                UIManager.Instance.SaveLoadUI.Show();
                UIManager.Instance.SaveLoadPanel.transform.SetAsLastSibling();
                AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            });
            if (SaveSystem.Instance.HasSaveData())
                _continueButton.gameObject.SetActive(true);
            else
                _continueButton.gameObject.SetActive(false);

            _startButton = _mainMenuPanel.transform.GetChild(1).GetChild(1).GetComponent<Button>();
            _startButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
                _LoadNextScene();
            });

            _optionButton = _mainMenuPanel.transform.GetChild(1).GetChild(2).GetComponent<Button>();
            _optionButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
                UIManager.Instance.OptionUI.Show();
                UIManager.Instance.OptionUI.transform.SetAsLastSibling();
            });

            _exitGameButton = _mainMenuPanel.transform.GetChild(1).GetChild(3).GetComponent<Button>();
            _exitGameButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
                Application.Quit();
            });

            AudioManager.Instance.PlayBGM(AudioManager.eBGM.MAIN);
        }

        private void OnDestroy()
        {
            Destroy(_mainMenuPanel);
        }
    }
}