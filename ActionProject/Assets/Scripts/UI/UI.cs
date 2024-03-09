using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public class UI : MonoBehaviour
    {
        public virtual void Initialize()
        {

        }

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

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            
        }
    }

}
