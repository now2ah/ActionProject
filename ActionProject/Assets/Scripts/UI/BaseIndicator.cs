using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class BaseIndicator : UI
    {
        Image _IndicatorImage;
        public Image IndicatorImage => _IndicatorImage;

        public override void SetParent( Transform parent)
        {
            _IndicatorImage.rectTransform.SetParent(parent, false);
        }

        private void Start()
        {
            _IndicatorImage = GetComponent<Image>();
        }
    }
}