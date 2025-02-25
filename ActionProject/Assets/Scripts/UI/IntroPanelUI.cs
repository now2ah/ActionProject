using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action.UI
{
    public class IntroPanelUI : UI
    {
        public override void Initialize()
        {
            base.Initialize();
            UIName = "IntroPanel";
            SceneType = Enums.eScene.INTRO;
            CanvasType = Enums.eCanvasType.MAIN;
        }
    }

}