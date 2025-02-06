using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public abstract class UI : MonoBehaviour
    {
        protected bool _isShow = false;
        public bool isShow => _isShow;

        public abstract void Initialize();

        public void Show()
        {
            _isShow = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _isShow = false;
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
