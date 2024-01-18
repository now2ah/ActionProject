using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using TMPro;

namespace Action.UI
{
    public class TownStagePanel : MonoBehaviour
    {
        GameObject _goldPanel;
        GameObject _foodPanel;
        GameObject _woodPanel;
        GameObject _ironPanel;
        TextMeshProUGUI _goldText;
        TextMeshProUGUI _foodText;
        TextMeshProUGUI _woodText;
        TextMeshProUGUI _ironText;

        private void Awake()
        {
            _goldPanel = transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            _foodPanel = transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
            _woodPanel = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
            _ironPanel = transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
            _goldText = _goldPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _foodText = _foodPanel.GetComponentInChildren<TextMeshProUGUI>();
            _woodText = _woodPanel.GetComponentInChildren<TextMeshProUGUI>();
            _ironText = _ironPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void RefreshResource()
        {
            _goldText.text = GameManager.Instance.Resource.Gold.ToString();
            _foodText.text = GameManager.Instance.Resource.Food.ToString();
            _woodText.text = GameManager.Instance.Resource.Wood.ToString();
            _ironText.text = GameManager.Instance.Resource.Iron.ToString();
        }
    }

}