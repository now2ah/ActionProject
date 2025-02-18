using Action.Manager;
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
        Enums.eCanvasType _canvasType;
        bool _isShow;

        public string UIName { get => _uiName; set => _uiName = value; }
        public Enums.eScene SceneType { get => _sceneType; set => _sceneType = value; }
        public Enums.eCanvasType CanvasType { get => _canvasType; set => _canvasType = value; }
        public bool isShow => _isShow;

        public virtual void Initialize()
        {
            _uiName = "";
            _sceneType = Enums.eScene.NONE;
            _canvasType = Enums.eCanvasType.NONE;
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

        public void AddToCanvas()
        {
            switch (_canvasType)
            {
                case Enums.eCanvasType.MAIN:
                    transform.SetParent(UIManager.Instance.MainCanvas.transform, false);
                    break;
                case Enums.eCanvasType.INGAME:
                    transform.SetParent(UIManager.Instance.InGameCanvas.transform, false);
                    break;
                default:
                    Logger.LogWarning(_uiName + "doesn't fixed canvas type");
                    break;
            }
        }

        public virtual void AddToCanvas(CanvasUI canvas)
        {
            if (null != canvas)
            {
                transform.SetParent(canvas.transform, false);
            }
        }

        public void SetPriorityTop()
        {
            transform.SetAsLastSibling();
        }

        protected virtual void Awake()
        {
            Initialize();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {

        }
    }

}
