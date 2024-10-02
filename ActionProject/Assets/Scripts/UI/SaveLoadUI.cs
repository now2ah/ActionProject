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

        public override void Initialize()
        {
            base.Initialize();
            _mode = eMode.NONE;
            _titleText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _buttonSlots = new Button[5];
            for (int i=0; i<5; i++)
            {
                _buttonSlots[i].onClick.AddListener(_OnClick(i));
                _dataSlotText[i] = transform.GetChild(2).GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
            }
        }

        UnityAction _OnClick(int num)
        {
            Logger.Log("SaveLoad : " + num);
            if (eMode.LOAD == _mode)
            {
                SaveSystem.Instance.Load(num);
            }
            else if (eMode.SAVE == _mode)
            {
                SaveSystem.Instance.Save(num, GameManager.Instance.GetWrappedGameData());
            }
            gameObject.SetActive(false);
            return null;
        }

        protected override void Awake()
        {
            base.Awake();

        }
    }
}

