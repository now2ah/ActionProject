using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class BaseIndicator : UI
    {
        public Image IndicatorImage;

        public override void SetParent( Transform parent)
        {
            IndicatorImage.rectTransform.SetParent(parent, false);
        }
    }
}