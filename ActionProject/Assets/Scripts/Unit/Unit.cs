using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.UI;
using Action.Manager;

namespace Action.Units
{
    public class Unit : MonoBehaviour
    {
        GameObject _infoPanel;
        public GameObject InfoPanel { get { return _infoPanel; } set { _infoPanel = value; } }
        string unitName = "none";
        public string UnitName { get { return unitName; } set { unitName = value; } }
        int _hp = 0;
        public int HP { get { return _hp; } set { _hp = value; } }
        int _fullHp = 0;
        public int FullHp { get { return _fullHp; } set { _fullHp = value; } }

        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public void ShowInfoPanel(bool isOn)
        {
            _infoPanel.SetActive(isOn);
        }

        public void GetDamaged(int damage)
        {
            _hp -= damage;
            if (_hp < 0)
                _Death();
        }

        void _Death()
        {

        }

        protected virtual void Start()
        {
            _infoPanel = UIManager.Instance.CreateUI("UnitInfoPanel", UIManager.Instance.InGameCanvas);
            UnitInfoPanel infoPanel = _infoPanel.GetComponent<UnitInfoPanel>();
            infoPanel.Initialize(this.gameObject);

            _stateMachine = new StateMachine();
        }

        protected virtual void Update()
        {
            if(null != _stateMachine)
                _stateMachine.Update();
        }
    }
}