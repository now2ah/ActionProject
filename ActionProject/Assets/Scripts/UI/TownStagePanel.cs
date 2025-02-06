using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Game;
using TMPro;

namespace Action.UI
{
    public class TownStagePanel : UI
    {
        GameObject _goldPanel;
        GameObject _foodPanel;
        GameObject _woodPanel;
        GameObject _ironPanel;
        GameObject _timerPanel;
        TextMeshProUGUI _goldText;
        TextMeshProUGUI _foodText;
        TextMeshProUGUI _woodText;
        TextMeshProUGUI _ironText;
        TextMeshProUGUI _timerText;

        public override void Initialize()
        {
        }

        protected override void Awake()
        {
            base.Awake();
            _goldPanel = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            _foodPanel = transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
            _woodPanel = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
            _ironPanel = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
            _timerPanel = transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
            _goldText = _goldPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _foodText = _foodPanel.GetComponentInChildren<TextMeshProUGUI>();
            _woodText = _woodPanel.GetComponentInChildren<TextMeshProUGUI>();
            _ironText = _ironPanel.GetComponentInChildren<TextMeshProUGUI>();
            _timerText = _timerPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void RefreshResource()
        {
            _goldText.text = GameManager.Instance.GameData.resource.Gold.ToString();
            _foodText.text = GameManager.Instance.GameData.resource.Food.ToString();
            _woodText.text = GameManager.Instance.GameData.resource.Wood.ToString();
            _ironText.text = GameManager.Instance.GameData.resource.Iron.ToString();
        }

        public void RefreshTimer()
        {
            _timerText.text = GameManager.Instance.PhaseTimer.GetTimeString();
        }
    }

}