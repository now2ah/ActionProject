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

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void SetParent(Transform parent)
        {
            _IndicatorImage.rectTransform.SetParent(parent, false);
        }

        protected override void Awake()
        {
            base.Awake();
            _IndicatorImage = GetComponent<Image>();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}