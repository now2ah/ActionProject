using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Action.Manager;
using TMPro;

namespace Action.UI
{
    public class OptionUI : UI
    {
        public TMP_Dropdown resolutionDropdown;
        public Toggle windowedToggle;
        public Slider bgmSlider;
        public TextMeshProUGUI bgmText;
        public Slider sfxSlider;
        public TextMeshProUGUI sfxText;
        public Button backButton;
        public GameObject windowModeCheckBox;

        bool _isWindowed = false;

        public override void Initialize()
        {
            
        }

        void _OnResolutionChanged(TMP_Dropdown change)
        {
            change.value = resolutionDropdown.value;

            switch (change.value)
            {
                case 0:
                    Screen.SetResolution(1920, 1080, !_isWindowed);
                    break;
                case 1:
                    Screen.SetResolution(1600, 900, !_isWindowed);
                    break;
                case 2:
                    Screen.SetResolution(1280, 720, !_isWindowed);
                    break;
            }
        }

        void _OnWindowedToggleChanged(bool isOn)
        {
            _isWindowed = isOn;
            if (isOn)
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                windowModeCheckBox.SetActive(true);
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                windowModeCheckBox.SetActive(false);
            }
                
        }

        void _OnBGMSliderChanged(float volume)
        {
            AudioManager.Instance.BGMVolume = volume;
            AudioManager.Instance.onBgmVolumeChanged.Invoke();
            bgmText.text = Mathf.Floor(volume * 100).ToString() + "%";
            
        }

        void _OnSFXSliderChanged(float volume)
        {
            AudioManager.Instance.SFXVolume = volume;
            AudioManager.Instance.onSfxVolumeChanged.Invoke();
            sfxText.text = Mathf.Floor(volume * 100).ToString() + "%";
        }

        private void OnEnable()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        protected override void Awake()
        {
            base.Awake();
            resolutionDropdown.onValueChanged.AddListener(delegate { _OnResolutionChanged(resolutionDropdown); });
            windowedToggle.onValueChanged.AddListener(delegate { _OnWindowedToggleChanged(windowedToggle.isOn); });
            bgmSlider.onValueChanged.AddListener(delegate { _OnBGMSliderChanged(bgmSlider.value); });
            sfxSlider.onValueChanged.AddListener(delegate { _OnSFXSliderChanged(sfxSlider.value); });
            backButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        protected override void Update()
        {
            base.Update();

        }
    }

}
