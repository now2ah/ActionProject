using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class MenuPanelUI : UI
    {
        //temporary naming
        Button _gameMode1Button;

        public override void Initialize()
        {
            base.Initialize();
            UIName = "MenuPanel";
            SceneType = Enums.eScene.MENU;
            CanvasType = Enums.eCanvasType.MAIN;

            _BindingButtons();

            _gameMode1Button.onClick.AddListener(_GameMode1Button);

        }

        void _BindingButtons()
        {
            Transform gameMode = transform.GetChild(0);
            if (gameMode.GetChild(0).TryGetComponent<Button>(out Button gameMode1Button))
            {
                _gameMode1Button = gameMode1Button;
            }
        }

        //temporary naming
        void _GameMode1Button()
        {
            Logger.Log("GameMode 1");
        }
    }
}

