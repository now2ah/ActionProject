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
        public Canvas MainCanvas => _mainCanvas;
        GameObject _BaseIndicatorObj;
        BaseIndicator _BaseIndicator;

        bool _isShowUnitPanel = true;
        public bool IsShowUnitPanel { get { return _isShowUnitPanel; } set { _isShowUnitPanel = value; } }

        GameObject _townStageUI;
        public GameObject TownStageUI { get { return _townStageUI; } set { _townStageUI = value; } }
        TownStagePanel _townStagePanel;

        public override void Initialize()
        {
            base.Initialize();
            _CreateMainCanvas();
            _BaseIndicatorObj = CreateUI("BaseIndicator");
            _BaseIndicator = _BaseIndicatorObj.GetComponent<BaseIndicator>();
            _BaseIndicator.Hide();
        }

        public GameObject CreateUI(string name)
        {
            string uiPath = "Prefabs/UI/";
            GameObject obj = Instantiate(Resources.Load(uiPath + name) as GameObject);

            if (null == obj)
                Debug.LogError("UI Prefab is missing.");

            if (null != _mainCanvas)
                obj.transform.SetParent(_mainCanvas.transform, false);

            return obj;
        }

        public void ShowUnitInfoUI(bool isOn)
        {
            if (_isShowUnitPanel == isOn)
                return;

            foreach(GameObject obj in GameManager.Instance.PlayerBuildings)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                    comp.ShowInfoPanel(isOn);
            }

            foreach (GameObject obj in GameManager.Instance.PlayerUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                    comp.ShowInfoPanel(isOn);
            }

            foreach (GameObject obj in GameManager.Instance.MonsterUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                    comp.ShowInfoPanel(isOn);
            }

            _isShowUnitPanel = isOn;
        }

        public void SetUnitInfoRect(float width, float height)
        {
            foreach (GameObject obj in GameManager.Instance.PlayerBuildings)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.InfoPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }

            foreach (GameObject obj in GameManager.Instance.PlayerUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.InfoPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }

            foreach (GameObject obj in GameManager.Instance.MonsterUnits)
            {
                if (obj.TryGetComponent<Unit>(out Unit comp))
                {
                    if (comp.InfoPanel.TryGetComponent<InGameUI>(out InGameUI ui))
                    {
                        ui.ApplyRect(width, height);
                    }
                }
            }
        }

        void _CreateMainCanvas()
        {
            _mainCanvasObject = Instantiate(Resources.Load("Prefabs/UI/MainCanvasObject") as GameObject);
            _mainCanvasObject.transform.SetParent(this.transform, false);
            _mainCanvas = _mainCanvasObject.GetComponentInChildren<Canvas>();
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

                    if (_mainCanvas.TryGetComponent<RectTransform>(out RectTransform screenRect))
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

        public void CreateTownStagePanel()
        {
            _townStageUI = CreateUI("TownStagePanel");
            _townStagePanel = _townStageUI.GetComponent<TownStagePanel>();
            _townStagePanel.RefreshResource();
        }

        private void Update()
        {
            _CalculateOffScreenIndicator();
        }
    }
}