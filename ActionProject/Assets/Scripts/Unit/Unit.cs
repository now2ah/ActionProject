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
        int _maxHp = 0;
        public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
        float _speed;
        public float Speed { get { return _speed; } set { _speed = value; } }

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