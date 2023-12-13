using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class UI : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetParent(Transform tr)
        {
            transform.SetParent(tr);
        }
    }

}
