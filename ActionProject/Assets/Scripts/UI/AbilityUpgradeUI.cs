using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Action.Manager;
using Action.Game;
using TMPro;

namespace Action.UI
{
    public class AbilityUpgradeUI : UI
    {
        public GameObject[] items;

        List<Ability> _abilityList;
        Button[] _chooseButton;

        public void Initialize(List<Ability> abilityList)
        {
            _abilityList = abilityList;
            for (int i = 0; i < _abilityList.Count; i++)
            {
                TextMeshProUGUI nameText = items[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI descText = items[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                nameText.text = _abilityList[i].AbilityName;
                descText.text = _abilityList[i].Description;

                _chooseButton[i] = items[i].transform.GetChild(2).GetComponent<Button>();
            }
            _chooseButton[0].onClick.AddListener(() => _ChooseItem(0));
            _chooseButton[1].onClick.AddListener(() => _ChooseItem(1));
            _chooseButton[2].onClick.AddListener(() => _ChooseItem(2));
            Show();
            GameManager.Instance.Stop();
        }

        void _ChooseItem(int i)
        {
            Logger.Log(i.ToString());
            _abilityList[i].LevelUp(_abilityList[i].Level + 1);
            Hide();
            GameManager.Instance.Resume();
        }

        protected override void Awake()
        {
            base.Awake();
            _chooseButton = new Button[3];
        }
    }
}

