using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;

namespace Action.UI
{
    public class GameOverUI : UI
    {
        Button _toMainButton;

        protected override void Awake()
        {
            base.Awake();
            _toMainButton = transform.GetChild(1).GetComponent<Button>();
            _toMainButton.onClick.AddListener(_ToMainMenu);
        }

        void _ToMainMenu()
        {
            GameManager.Instance.ResetGame();
            GameManager.Instance.Resume();
            SceneManager.Instance.LoadGameScene(1);
        }
    }

}
