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

        public override void Initialize()
        {
        }

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

        protected void OnEnable()
        {
            if (GameManager.Instance.PhaseStateMachine.CurState == GameManager.Instance.HuntState ||
                GameManager.Instance.PhaseStateMachine.CurState == GameManager.Instance.DefenseState)
            {
                _saveButton.gameObject.SetActive(false);
            }
        }

        void OnResumeButton()
        {
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            GameManager.Instance.Resume();
            gameObject.SetActive(false);
        }

        void OnSaveButton()
        {
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            UIManager.Instance.SaveLoadUI.Initialize(SaveLoadUI.eMode.SAVE);
            UIManager.Instance.SaveLoadUI.Show();
            UIManager.Instance.SaveLoadUI.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }

        void OnLoadButton()
        {
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            UIManager.Instance.SaveLoadUI.Initialize(SaveLoadUI.eMode.LOAD);
            UIManager.Instance.SaveLoadUI.Show();
            UIManager.Instance.SaveLoadUI.transform.SetAsLastSibling();
            gameObject.SetActive(false);
        }

        void OnQuitButton()
        {
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            GameManager.Instance.Resume();
            GameManager.Instance.ResetGame();
            SceneManager.Instance.LoadGameScene(Enums.eScene.INTRO);
            gameObject.SetActive(false);
        }
    }
}

