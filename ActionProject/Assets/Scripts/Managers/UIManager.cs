using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Action.Util;
using Action.UI;
using Action.Units;

namespace Action.Manager
{
    public class UIManager : Singleton<UIManager>
    {
        public enum eFade
        {
            FadeIn,
            FadeOut,
        }

        GameObject _mainCanvasObject;
        Canvas _mainCanvas;

        GameObject _inGameCanvasObject;
        Canvas _inGameCanvas;

        GameObject _eventSystemObject;

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

        public Canvas MainCanvas => _mainCanvas;
        public Canvas InGameCanvas => _inGameCanvas;
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
            base.Initialize();
            _CreateMainCanvas();
            _CreateInGameCanvas();
            _CreateEventSystem();
            _BaseIndicatorObj = CreateUI("BaseIndicator", _inGameCanvas);
            _BaseIndicator = _BaseIndicatorObj.GetComponent<BaseIndicator>();
            _BaseIndicator.Hide();
            _fadeUIPanel = CreateUI("FadeInNOutPanel", _mainCanvas);
            _damagedEffectPanel = CreateUI("DamagedEffectPanel", _mainCanvas);
            _fadeUIPanel.SetActive(false);
            _damagedEffectPanel.SetActive(false);
            _expPanel = CreateUI("ExpPanel", _mainCanvas);
            _expBarUI = _expPanel.GetComponent<ExpBarUI>();
            _expBarUI.Hide();
            _abilityUpgradePanel = CreateUI("AbilityUpgradePanel", _mainCanvas);
            _abilityUpgradeUI = _abilityUpgradePanel.GetComponent<AbilityUpgradeUI>();
            _abilityUpgradeUI.Hide();
            _waveCamPanel = CreateUI("WaveCamPanel", _mainCanvas);
            _waveCamUI = _waveCamPanel.GetComponent<WaveCamUI>();
            _waveCamUI.Hide();
            _saveLoadPanel = CreateUI("SaveLoadPanel", _mainCanvas);
            _saveLoadUI = _saveLoadPanel.GetComponent<SaveLoadUI>();
            _saveLoadUI.Hide();
            _skillIconPanel = CreateUI("SkillIconPanel", _mainCanvas);
            _skillIconUI = _skillIconPanel.GetComponent<SkillIconUI>();
            _skillIconUI.Hide();
            _phaseTextPanel = CreateUI("PhaseTextPanel", _mainCanvas);
            _phaseTextUI = _phaseTextPanel.GetComponent<PhaseTextUI>();
            _phaseTextUI.Hide();
            _optionPanel = CreateUI("OptionPanel", _mainCanvas);
            _optionUI = _optionPanel.GetComponent<OptionUI>();
            _optionUI.Hide();
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
            _damagedEffectPanel.SetActive(true);
            Fade(eFade.FadeOut, _damagedEffectPanel, 0.1f, () =>
            {
                _damagedEffectPanel.SetActive(false);
            });
        }

        public void CreateTownStagePanel()
        {
            if (null == _townStageUI)
            {
                _townStageUI = CreateUI("TownStagePanel", UIManager.Instance.MainCanvas);
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

        public void Fade(eFade fade, GameObject fadeUI, float fadeSpeed, UnityAction action = null)
        {
            StopCoroutine("FadeCoroutine");
            StartCoroutine(FadeCoroutine(fade, fadeUI, fadeSpeed, action));
        }

        IEnumerator FadeCoroutine(eFade fade, GameObject fadeUI, float fadeSpeed, UnityAction action = null)
        {
            if (null != fadeUI)
            {
                fadeUI?.SetActive(true);
                fadeUI.transform.SetSiblingIndex(UIManager.Instance.MainCanvas.transform.childCount - 1);
                float startValue = (fade == eFade.FadeIn) ? 1f : 0f;
                float endValue = (fade == eFade.FadeIn) ? 0f : 1f;
                float alpha = startValue;

                Image fadeImage = fadeUI.GetComponent<Image>();

                if (fade == eFade.FadeIn)
                {
                    while (alpha >= endValue)
                    {
                        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fade == eFade.FadeIn) ? -1 : 1);
                        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                        yield return null;
                    }
                }
                else if (fade == eFade.FadeOut)
                {
                    while (alpha <= endValue)
                    {
                        alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fade == eFade.FadeIn) ? -1 : 1);
                        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                        yield return null;
                    }
                }
                action?.Invoke();
                fadeUI?.SetActive(false);
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