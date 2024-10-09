using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Manager;

namespace Action.UI
{
    public class ExpBarUI : UI
    {
        Image _fillImage;

        public override void Initialize()
        {
            base.Initialize();
            
        }

        public void ApplyExpValue(int exp, int fullExp)
        {
            if (0 == exp)
                _fillImage.fillAmount = 0;
            else
            _fillImage.fillAmount = Mathf.Clamp01((float)exp / (float)fullExp);
        }

        protected new void Awake()
        {
            _fillImage = gameObject.transform.GetChild(0).GetComponent<Image>();
        }

        protected new void Start()
        {

        }
    }

}