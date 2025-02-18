using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class FadePanelUI : UI
    {
        Image _fadeImage;

        public Image FadeImage { get { return _fadeImage; } }

        public override void Initialize()
        {
            base.Initialize();
            UIName = "FadePanelUI";
            SceneType = Enums.eScene.INTRO;
            CanvasType = Enums.eCanvasType.MAIN;

            if (TryGetComponent<Image>(out Image fadeImage))
            {
                _fadeImage = fadeImage;
            }
        }
    }

}