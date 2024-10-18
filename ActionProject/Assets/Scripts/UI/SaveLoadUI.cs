using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Action.Manager;


namespace Action.UI
{
    public class SaveLoadUI : UI
    {
        public enum eMode
        {
            NONE,
            SAVE,
            LOAD
        }

        eMode _mode;
        TextMeshProUGUI _titleText;
        Button[] _buttonSlots;
        TextMeshProUGUI[] _dataSlotText;
        Button _cancelButton;

        public eMode Mode { get { return _mode; } set { _mode = value; } }

        public void Initialize(eMode mode)
        {
            //transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _mode = mode;
            if (null == _titleText)
                _titleText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _buttonSlots = new Button[5];
            _dataSlotText = new TextMeshProUGUI[5];
            _cancelButton = transform.GetChild(2).GetComponent<Button>();
            for (int i=0; i<5; i++)
            {
                _buttonSlots[i] = transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<Button>();
                //_buttonSlots[i].onClick.AddListener(_OnClick(i));
                _dataSlotText[i] = transform.GetChild(1).GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            }
            _buttonSlots[0].onClick.AddListener(() => _OnClick(0));
            _buttonSlots[1].onClick.AddListener(() => _OnClick(1));
            _buttonSlots[2].onClick.AddListener(() => _OnClick(2));
            _buttonSlots[3].onClick.AddListener(() => _OnClick(3));
            _buttonSlots[4].onClick.AddListener(() => _OnClick(4));
            _cancelButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                if (GameManager.Instance.IsPaused)
                    GameManager.Instance.Resume();
            });

            if (mode == eMode.SAVE)
            {
                _titleText.text = "Select Save data slot";
            }
            else if (mode == eMode.LOAD)
            {
                _titleText.text = "Select Load data slot";
            }

            SaveSystem.Instance.LoadSourceData();

            _RefreshSlotText();
        }

        void _OnClick(int num)
        {
            AudioManager.Instance.PlaySFX(AudioManager.eSfx.CLICK);
            if (eMode.LOAD == _mode)
            {
                GameManager.Instance.Resume();
                SaveSystem.Instance.Load(num);
                
            }
            else if (eMode.SAVE == _mode)
            {
                GameManager.Instance.Resume();
                SaveSystem.Instance.Save(num, GameManager.Instance.GetWrappedGameData());
            }
            gameObject.SetActive(false);

            _RefreshSlotText();
        }

        void _RefreshSlotText()
        {
            for (int i = 0; i < 5; i++)
            {
                if (null != SaveSystem.Instance.DataSlots[i])
                    _dataSlotText[i].text = SaveSystem.Instance.DataSlots[i].date;
            }
        }

        private void OnEnable()
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        protected override void Awake()
        {
            base.Awake();

        }
    }
}

