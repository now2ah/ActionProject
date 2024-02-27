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
        GameObject _infoPanelObject;
        public GameObject InfoPanelObject { get { return _infoPanelObject; } set { _infoPanelObject = value; } }
        UnitPanel _unitPanel;
        public UnitPanel InfoPanel { get { return _unitPanel; } set { _unitPanel = value; } }

        string _unitName;
        public string UnitName { get { return _unitName; } set { _unitName = value; } }
        int _hp;
        public int HP { get { return _hp; } set { _hp = value; } }
        int _maxHp;
        public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
        float _speed;
        public float Speed { get { return _speed; } set { _speed = value; } }
        float _infoActiveDistant;
        public float InfoActiveDistant { get { return _infoActiveDistant; } set { _infoActiveDistant = value; } }

        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public virtual void Initialize()
        {
            _unitName = "default_name";
            _hp = 10;
            _maxHp = 10;
            _speed = 1;
            _infoActiveDistant = Constant.INGAMEUI_VISIBLE_DISTANT;
            _infoPanelObject = UIManager.Instance.CreateUI("UnitPanel", UIManager.Instance.InGameCanvas);
            _unitPanel = _infoPanelObject.GetComponent<UnitPanel>();
            _unitPanel.Initialize(this.gameObject);
            _unitPanel.Hide();

            _stateMachine = new StateMachine();
        }

        public void GetDamaged(int damage)
        {
            _hp -= damage;
            if (_hp < 0)
                _Death();
        }

        public void SetNameUI(string name)
        {
            if (null != _unitPanel)
                _unitPanel.SetNameText(name);
        }

        void _Death()
        {

        }

        protected bool _IsNearPlayerUnit()
        {
            float dist = Vector3.Distance(GameManager.Instance.PlayerUnitObj.transform.position, transform.position);

            if (dist < _infoActiveDistant)
                return true;
            else
                return false;
        }

        void _VisualizeUnitPanel()
        {
            if (_IsNearPlayerUnit())
                _unitPanel.Show();
            else
                _unitPanel.Hide();
        }

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Update()
        {
            _VisualizeUnitPanel();
            if (null != _stateMachine)
                _stateMachine.Update();
        }
    }
}