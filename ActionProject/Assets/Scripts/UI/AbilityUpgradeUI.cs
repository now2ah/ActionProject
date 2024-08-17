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
                _chooseButton[i].onClick.AddListener(() => _ChooseItem(i));
                _chooseButton[i].onClick.AddListener(Test);
            }
            Show();
            //GameManager.Instance.Stop();
        }
        void Test()
        {
            Logger.Log("clicked");
        }
        void _ChooseItem(int i)
        {
            _abilityList[i].LevelUp(_abilityList[i].Level + 1);
            Hide();
            //GameManager.Instance.Resume();
        }

        protected override void Awake()
        {
            base.Awake();
            _chooseButton = new Button[3];
        }
    }
}

