using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Action.UI
{
    public class BaseIndicator : Image
    {
        public void Hide() { gameObject.SetActive(false); }
        public void Show() { gameObject.SetActive(true); }

        public void SetParent( Transform parent)
        {
            rectTransform.SetParent(parent, false);
        }
    }
}