using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Util;
using Action.UI;
using Action.Units;

namespace Action.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        GameObject _mainCanvasObject;
        Canvas _mainCanvas;

        GameObject _inGameCanvasObject;
        Canvas _inGameCanvas;

        GameObject _eventSystemObject;

        GameObject _BaseIndicatorObj;
        BaseIndicator _BaseIndicator;

        GameObject _damagedEffectPanel;

        bool _isShowUnitPanel = true;

        GameObject _townStageUI;
        TownStagePanel _townStagePanel;

        public Canvas MainCanvas => _mainCanvas;
        public Canvas InGameCanvas => _inGameCanvas;
        public GameObject DamagedEffectPanel => _damagedEffectPanel;
        public bool IsShowUnitPanel { get { return _isShowUnitPanel; } set { _isShowUnitPanel = value; } }
        public GameObject TownStageUI { get { return _townStageUI; } set { _townStageUI = value; } }

        public override void Initialize()
        {
            base.Initialize();
            _CreateMainCanvas();
            _CreateInGameCanvas();
            _CreateEventSystem();
            _BaseIndicatorObj = CreateUI("BaseIndicator", _inGameCanvas);
            _BaseIndicator = _BaseIndicatorObj.GetComponent<BaseIndicator>();
            _BaseIndicator.Hide();
            _damagedEffectPanel = CreateUI("DamagedEffectPanel", _mainCanvas);
            _damagedEffectPanel.SetActive(false);
        }

        public GameObject CreateUI(string name, Canvas canvas)
        {
            string uiPath = "Prefabs/UI/";
            GameObject obj = Instantiate(Resources.Load(uiPath + name) as GameObject);

            if (null == obj)
                Debug.LogError("UI Prefab is missing.");

            if (null != canvas)
                obj.transform.SetParent(canvas.transform, false);

            return obj;
        }

        public void SetUnitInfoRect(float width, float height)
        {
            foreach (GameObject obj in GameManager.Instance.PlayerBuildings)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.UnitPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }

            foreach (GameObject obj in GameManager.Instance.PlayerUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.UnitPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }

            foreach (GameObject obj in GameManager.Instance.MonsterUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.UnitPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }
        }

        public void ShowDamagedEffect()
        {
            StopCoroutine(_DamagedEffectCoroutine());
            StartCoroutine(_DamagedEffectCoroutine());
        }

        public void CreateTownStagePanel()
        {
            _townStageUI = CreateUI("TownStagePanel", UIManager.Instance.MainCanvas);
            _townStagePanel = _townStageUI.GetComponent<TownStagePanel>();
            _townStagePanel.RefreshResource();
        }

        public void RefreshTownStageUI()
        {
            if (null != _townStagePanel)
            {
                _townStagePanel.RefreshResource();
                _townStagePanel.RefreshTimer();
            }
        }

        void _CreateMainCanvas()
        {
            _mainCanvasObject = Instantiate(Resources.Load("Prefabs/UI/CanvasObject") as GameObject);
            _mainCanvasObject.name = "MainCanvas";
            _mainCanvasObject.transform.SetParent(this.transform, false);
            _mainCanvas = _mainCanvasObject.GetComponentInChildren<Canvas>();
        }

        void _CreateInGameCanvas()
        {
            _inGameCanvasObject = Instantiate(Resources.Load("Prefabs/UI/CanvasObject") as GameObject);
            _inGameCanvasObject.name = "InGameCanvas";
            _inGameCanvasObject.transform.SetParent(this.transform, false);
            _inGameCanvas = _inGameCanvasObject.GetComponentInChildren<Canvas>();
        }

        void _CreateEventSystem()
        {
            _eventSystemObject = Instantiate(Resources.Load("Prefabs/UI/EventSystem") as GameObject);
            _eventSystemObject.transform.SetParent(this.transform, false);
        }

        void _CalculateOffScreenIndicator()
        {
            if (null == _BaseIndicatorObj || null == GameManager.Instance.PlayerBase)
                return;

            Vector3 screenPos = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(GameManager.Instance.PlayerBase.transform.position);

            if (_BaseIndicatorObj.TryGetComponent<BaseIndicator>(out BaseIndicator comp))
            {
                if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
                {
                    comp.Show();
                    RectTransform rectTr = comp.IndicatorImage.rectTransform;

                    if (_inGameCanvas.TryGetComponent<RectTransform>(out RectTransform screenRect))
                    {
                        Vector2 halfSize = screenRect.rect.size / 2.0f;

                        float angle = Mathf.Atan2(screenPos.y - halfSize.y, screenPos.x - halfSize.x);

                        Vector3 position = new Vector3(Mathf.Cos(angle) * halfSize.x, Mathf.Sin(angle) * halfSize.y, 0.0f);
                        rectTr.localPosition = position;

                        Vector3 rotation = rectTr.localRotation.eulerAngles;
                        rotation.z = angle * Mathf.Rad2Deg;

                        rectTr.localRotation = Quaternion.Euler(rotation);
                    }
                }
                else
                {
                    comp.Hide();
                }
            }
        }

        IEnumerator _DamagedEffectCoroutine()
        {
            _damagedEffectPanel.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            _damagedEffectPanel.SetActive(false);
        }

        private void Update()
        {
            _CalculateOffScreenIndicator();
        }
    }
}