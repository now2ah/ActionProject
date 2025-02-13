using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action.UI
{
    public class LoadingPanelUI : UI
    {
        public override void Initialize()
        {
            base.Initialize();
            UIName = "LoadingPanel";
            SceneType = Enums.eScene.LOADING;
            CanvasType = Enums.eCanvasType.MAIN;
        }
    }

}