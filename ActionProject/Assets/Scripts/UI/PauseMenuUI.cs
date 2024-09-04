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
        Button _quitButton;

        protected override void Awake()
        {
            base.Awake();
            _resumeButton = transform.GetChild(1).GetComponent<Button>();
            _quitButton = transform.GetChild(2).GetComponent<Button>();
            _resumeButton.onClick.AddListener(OnResumeButton);
            _quitButton.onClick.AddListener(OnQuitButton);
        }

        void OnResumeButton()
        {
            GameManager.Instance.Resume();
            gameObject.SetActive(false);
        }

        void OnQuitButton()
        {
            SceneManager.Instance.LoadGameScene(1);
        }
    }
}

