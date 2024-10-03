using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using TMPro;


namespace Action.UI
{
    public class PauseMenuUI : UI
    {
        Button _resumeButton;
        Button _saveButton;
        Button _loadButton;
        Button _quitButton;

        protected override void Awake()
        {
            base.Awake();
            _resumeButton = transform.GetChild(1).GetComponent<Button>();
            _saveButton = transform.GetChild(2).GetComponent<Button>();
            _loadButton = transform.GetChild(3).GetComponent<Button>();
            _quitButton = transform.GetChild(4).GetComponent<Button>();
            _resumeButton.onClick.AddListener(OnResumeButton);
            _saveButton.onClick.AddListener(OnSaveButton);
            _loadButton.onClick.AddListener(OnLoadButton);
            _quitButton.onClick.AddListener(OnQuitButton);
        }

        void OnResumeButton()
        {
            GameManager.Instance.Resume();
            gameObject.SetActive(false);
        }

        void OnSaveButton()
        {
            UIManager.Instance.SaveLoadUI.Initialize(SaveLoadUI.eMode.SAVE);
            UIManager.Instance.SaveLoadUI.Show();
            UIManager.Instance.SaveLoadUI.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }

        void OnLoadButton()
        {
            UIManager.Instance.SaveLoadUI.Initialize(SaveLoadUI.eMode.LOAD);
            UIManager.Instance.SaveLoadUI.Show();
            UIManager.Instance.SaveLoadUI.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }

        void OnQuitButton()
        {
            GameManager.Instance.ResetGame();
            SceneManager.Instance.LoadGameScene(1);
            gameObject.SetActive(false);
        }
    }
}

