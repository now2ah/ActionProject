using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;
using Action.UI;
using Action.Units;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Xml.Linq;

namespace Action.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        #region PRIVATE

        CanvasUI _mainCanvas;
        CanvasUI _inGameCanvas;
        EventSystem _eventSystem;
        List<GameObject> _miscUIAssets;
        Dictionary<string, UI.UI> _miscUIDic;
        GameObject _miscUIGameObject;

        #endregion

        public enum eFade
        {
            FadeIn,
            FadeOut,
        }

        GameObject _BaseIndicatorObj;
        BaseIndicator _BaseIndicator;

        GameObject _fadeUIPanel;
        GameObject _damagedEffectPanel;

        bool _isShowUnitPanel = true;

        GameObject _townStageUI;
        TownStagePanel _townStagePanel;

        GameObject _expPanel;
        ExpBarUI _expBarUI;
        GameObject _abilityUpgradePanel;
        AbilityUpgradeUI _abilityUpgradeUI;
        GameObject _pausePanel;
        GameObject _waveCamPanel;
        WaveCamUI _waveCamUI;
        GameObject _saveLoadPanel;
        SaveLoadUI _saveLoadUI;
        GameObject _skillIconPanel;
        SkillIconUI _skillIconUI;
        GameObject _phaseTextPanel;
        PhaseTextUI _phaseTextUI;
        GameObject _optionPanel;
        OptionUI _optionUI;

        public CanvasUI MainCanvas => _mainCanvas;
        public CanvasUI InGameCanvas => _inGameCanvas;
        public EventSystem EventSystem => _eventSystem;
        public Dictionary<string, UI.UI> MiscUIDic => _miscUIDic;

        public BaseIndicator BaseIndicatorUI => _BaseIndicator;
        public GameObject FadeUIPanel => _fadeUIPanel;
        public GameObject DamagedEffectPanel => _damagedEffectPanel;
        public bool IsShowUnitPanel { get { return _isShowUnitPanel; } set { _isShowUnitPanel = value; } }
        public GameObject TownStageUI { get { return _townStageUI; } set { _townStageUI = value; } }
        public TownStagePanel TownStagePanel { get { return _townStagePanel; } set { _townStagePanel = value; } }
        public GameObject ExpPanel { get { return _expPanel; } set { _expPanel = value; } }
        public ExpBarUI ExpBarUI { get { return _expBarUI; } set { _expBarUI = value; } }
        public GameObject AbilityUpgradePanel { get { return _abilityUpgradePanel; } set { _abilityUpgradePanel = value; } }
        public AbilityUpgradeUI AbilityUpgradeUI { get { return _abilityUpgradeUI; } set { _abilityUpgradeUI = value; } }
        public GameObject PausePanel { get { return _pausePanel; } set { _pausePanel = value; } }
        public GameObject WaveCamPanel { get { return _waveCamPanel; } set { _waveCamPanel = value; } }
        public WaveCamUI WaveCamUI { get { return _waveCamUI; } set { _waveCamUI = value; } }
        public GameObject SaveLoadPanel { get { return _saveLoadPanel; } set { _saveLoadPanel = value; } }
        public SaveLoadUI SaveLoadUI { get { return _saveLoadUI; } set { _saveLoadUI = value; } }
        public SkillIconUI SkillIconUI { get { return _skillIconUI; } set { _skillIconUI = value; } }
        public PhaseTextUI PhaseTextUI { get { return _phaseTextUI; } set { _phaseTextUI = value; } }
        public OptionUI OptionUI { get { return _optionUI; } set { _optionUI = value; } }

        public override void Initialize()
        {
            _CreateMainCanvas();
            _CreateInGameCanvas();
            _CreateEventSystem();

            _LoadMiscUIs();
            

            //_BaseIndicatorObj = CreateUI("BaseIndicator", _inGameCanvas);
            //_BaseIndicator = _BaseIndicatorObj.GetComponent<BaseIndicator>();
            //_BaseIndicator.Hide();
            //_fadeUIPanel = CreateUI("FadeInNOutPanel", _mainCanvas);
            //_damagedEffectPanel = CreateUI("DamagedEffectPanel", _mainCanvas);
            //_fadeUIPanel.SetActive(false);
            //_damagedEffectPanel.SetActive(false);
            //_expPanel = CreateUI("ExpPanel", _mainCanvas);
            //_expBarUI = _expPanel.GetComponent<ExpBarUI>();
            //_expBarUI.Hide();
            //_abilityUpgradePanel = CreateUI("AbilityUpgradePanel", _mainCanvas);
            //_abilityUpgradeUI = _abilityUpgradePanel.GetComponent<AbilityUpgradeUI>();
            //_abilityUpgradeUI.Hide();
            //_waveCamPanel = CreateUI("WaveCamPanel", _mainCanvas);
            //_waveCamUI = _waveCamPanel.GetComponent<WaveCamUI>();
            //_waveCamUI.Hide();
            //_saveLoadPanel = CreateUI("SaveLoadPanel", _mainCanvas);
            //_saveLoadUI = _saveLoadPanel.GetComponent<SaveLoadUI>();
            //_saveLoadUI.Hide();
            //_skillIconPanel = CreateUI("SkillIconPanel", _mainCanvas);
            //_skillIconUI = _skillIconPanel.GetComponent<SkillIconUI>();
            //_skillIconUI.Hide();
            //_phaseTextPanel = CreateUI("PhaseTextPanel", _mainCanvas);
            //_phaseTextUI = _phaseTextPanel.GetComponent<PhaseTextUI>();
            //_phaseTextUI.Hide();
            //_optionPanel = CreateUI("OptionPanel", _mainCanvas);
            //_optionUI = _optionPanel.GetComponent<OptionUI>();
            //_optionUI.Hide();
        }

        public UI.UI GetMiscUI(string uiName)
        {
            UI.UI ret = null;
            if (null != _miscUIDic && _miscUIDic.Count > 0)
            {
                ret = _miscUIDic[uiName];
            }
            return ret;
        }

        public void ReturnToMiscUI(UI.UI ui)
        {
            if (null != _miscUIDic && _miscUIDic.Count > 0)
            {
                ui.transform.SetParent(_miscUIGameObject.transform);
                _miscUIDic[ui.UIName] = ui;
            }
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

            foreach (GameObject obj in GameManager.Instance.EnemyUnits)
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
            //_damagedEffectPanel.SetActive(true);
            //Fade(eFade.FadeOut, _damagedEffectPanel, 0.1f, () =>
            //{
            //    _damagedEffectPanel.SetActive(false);
            //});
        }

        public void CreateTownStagePanel()
        {
            if (null == _townStageUI)
            {
                //_townStageUI = CreateUI("TownStagePanel", UIManager.Instance.MainCanvas);
                _townStagePanel = _townStageUI.GetComponent<TownStagePanel>();
                _townStagePanel.RefreshResource();
            }
        }

        public void RefreshTownStageUI()
        {
            if (null != _townStagePanel)
            {
                _townStagePanel.RefreshResource();
                _townStagePanel.RefreshTimer();
            }
        }

        public async UniTask Fade(eFade fade, float fadeSpeed)
        {
            FadePanelUI fadeUI = GetMiscUI("FadePanelUI") as FadePanelUI;

            fadeUI.AddToCanvas(MainCanvas);
            fadeUI.SetPriorityTop();
            fadeUI.Show();

            if (fade == eFade.FadeIn)
            {
                float endValue = 0f;
                
                Color newColor = fadeUI.FadeImage.color;
                float alpha = newColor.a;
                
                while (alpha > endValue)
                {
                    alpha += -1f * fadeSpeed * Time.deltaTime;
                    fadeUI.FadeImage.color = new Color(fadeUI.FadeImage.color.r, fadeUI.FadeImage.color.g, fadeUI.FadeImage.color.b, alpha);
                    await UniTask.Delay(1);
                }
                ReturnToMiscUI(fadeUI);
                fadeUI.Hide();
            }
            else if (fade == eFade.FadeOut)
            {
                float endValue = 1f;

                Color newColor = fadeUI.FadeImage.color;
                float alpha = newColor.a;

                while (alpha < endValue)
                {
                    alpha += 1f * fadeSpeed * Time.deltaTime;
                    fadeUI.FadeImage.color = new Color(fadeUI.FadeImage.color.r, fadeUI.FadeImage.color.g, fadeUI.FadeImage.color.b, alpha);
                    await UniTask.Delay(1);
                }
                ReturnToMiscUI(fadeUI);
                fadeUI.Hide();
            }
        }

        void _CreateEventSystem()
        {
            GameObject eventSystemObject = AssetManager.Instance.LoadAsset(eAssetType.UI, "EventSystem");
            eventSystemObject.transform.SetParent(this.transform, false);
            _eventSystem = eventSystemObject.GetComponent<EventSystem>();
        }


        void _CreateMainCanvas()
        {
            GameObject mainCanvasObject = AssetManager.Instance.LoadAsset(eAssetType.UI, "Canvas");
            mainCanvasObject.name = "MainCanvas";
            mainCanvasObject.transform.SetParent(this.transform, false);
            if (mainCanvasObject.TryGetComponent<CanvasUI>(out CanvasUI canvasUI))
            {
                _mainCanvas = canvasUI;
            }
            else
            {
                _mainCanvas = mainCanvasObject.AddComponent<CanvasUI>();
            }
        }

        void _CreateInGameCanvas()
        {
            GameObject inGameCanvasObject = AssetManager.Instance.LoadAsset(eAssetType.UI, "Canvas");
            inGameCanvasObject.name = "InGameCanvas";
            inGameCanvasObject.transform.SetParent(this.transform, false);
            if (inGameCanvasObject.TryGetComponent<CanvasUI>(out CanvasUI canvasUI))
            {
                _inGameCanvas = canvasUI;
            }
            else
            {
                _inGameCanvas = inGameCanvasObject.AddComponent<CanvasUI>();
            }
        }

        void _LoadMiscUIs()
        {
            if (null == _miscUIAssets)
            {
                _miscUIAssets = new List<GameObject>();
            }

            if (null == _miscUIDic)
            {
                _miscUIDic = new Dictionary<string, UI.UI>();
            }
            
            if (null == _miscUIGameObject)
            {
                _miscUIGameObject = new GameObject("MiscUIObject");
                _miscUIGameObject.transform.SetParent(this.transform);
            }
            
            //load misc ui assets here
            _miscUIAssets.Add(AssetManager.Instance.LoadAsset(eAssetType.UI, "FadePanel"));

            foreach(var uiAsset in _miscUIAssets)
            {
                uiAsset.transform.SetParent(_miscUIGameObject.transform);
                if (uiAsset.TryGetComponent<UI.UI>(out UI.UI ui))
                {
                    ui.Initialize();
                    _miscUIDic.Add(ui.UIName, ui);
                }
            }
        }

        void _CalculateOffScreenIndicator()
        {
            if (null == _BaseIndicatorObj || null == GameManager.Instance.PlayerBase ||
                GameManager.Instance.PhaseStateMachine.CurState == GameManager.Instance.HuntState)
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

        private void Update()
        {
            _CalculateOffScreenIndicator();
        }
    }
}