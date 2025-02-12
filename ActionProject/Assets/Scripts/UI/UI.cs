using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.UI
{
    public abstract class UI : MonoBehaviour
    {
        string _uiName;
        Enums.eScene _sceneType;
        bool _isShow;

        public string UIName { get => _uiName; set => _uiName = value; }
        public Enums.eScene SceneType { get => _sceneType; set => _sceneType = value; }
        public bool isShow => _isShow;

        public virtual void Initialize()
        {
            _uiName = "";
            _sceneType = Enums.eScene.NONE;
            _isShow = false;
            gameObject.SetActive(_isShow);
        }

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
    }

}
