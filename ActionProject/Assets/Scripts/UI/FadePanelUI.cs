using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class FadePanelUI : UI
    {
        public override void Initialize()
        {
            base.Initialize();
            UIName = "FadePanel";
            SceneType = Enums.eScene.INTRO;
            CanvasType = Enums.eCanvasType.MAIN;
        }
    }

}