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
            if (null != tr)
            {
                transform.SetParent(tr);
            }
        }

        public virtual void AddToCanvas(CanvasUI canvas)
        {
            if (null != canvas)
            {
                transform.SetParent(canvas.transform);
            }
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
