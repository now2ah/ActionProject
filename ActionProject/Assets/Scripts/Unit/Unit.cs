using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.UI;
using Action.Manager;

namespace Action.Units
{
    public partial class Unit : MonoBehaviour
    {
        GameObject _unitPanelObject;
        UnitPanel _unitPanel;
        Collider _unitCollider;

        float _hp;
        float _maxHp;
        float _speed;

        string _unitName;
        bool _canDamaged;
        float _infoActiveDistant;

        bool _isOnUnitPanel;
        
        public GameObject UnitPanelObject { get { return _unitPanelObject; } set { _unitPanelObject = value; } }
        public UnitPanel UnitPanel { get { return _unitPanel; } set { _unitPanel = value; } }
        public string UnitName { get { return _unitName; } set { _unitName = value; } }
        public Collider UnitCollider { get { return _unitCollider; } set { _unitCollider = value; } }
        public float HP { get { return _hp; } set { _hp = value; } }
        public float MaxHp { get { return _maxHp; } set { _maxHp = value; } }
        public float Speed { get { return _speed; } set { _speed = value; } }
        public float InfoActiveDistant { get { return _infoActiveDistant; } set { _infoActiveDistant = value; } }
        public bool IsOnUnitPanel { get { return _isOnUnitPanel; } set { _isOnUnitPanel = value; } }

        StateMachine _stateMachine;
        public StateMachine StateMachine => _stateMachine;

        public virtual void Initialize()
        {
            _infoActiveDistant = Constant.INGAMEUI_VISIBLE_DISTANT;
            _unitPanelObject = UIManager.Instance.CreateUI("UnitPanel", UIManager.Instance.InGameCanvas);
            _unitPanel = _unitPanelObject.GetComponent<UnitPanel>();
            _unitPanel.Initialize(this.gameObject);
            _unitPanel.Hide();
            _unitCollider = GetComponentInChildren<Collider>();

            _stateMachine = new StateMachine();
        }


        public virtual void ApplyDamage(DamageMessage msg)
        {
            if (_CheckDead())
                return;

            _hp -= msg.amount;
            Logger.Log("Apply Damage : " + msg.amount + " (" + _hp + "/" + _maxHp + ")");
            if (_CheckDead())
            {
                _Death();
            }
        }

        public void SetNameUI(string name)
        {
            if (null != _unitPanel)
                _unitPanel.SetNameText(name);
        }

        bool _CheckDead()
        {
            if (0 > _hp)
                return true;
            else
                return false;
        }

        void _Death()
        {

        }

        protected bool _IsNearPlayerUnit()
        {
            float dist = Vector3.Distance(GameManager.Instance.CommanderObj.transform.position, transform.position);

            if (dist < _infoActiveDistant)
                return true;
            else
                return false;
        }

        void _VisualizeUnitPanel()
        {
            if (!_isOnUnitPanel || this.gameObject == GameManager.Instance.CommanderObj)
                return;

            if (_IsNearPlayerUnit())
                _unitPanel.Show();
            else
                _unitPanel.Hide();
        }

        protected virtual void Awake()
        {
            _unitName = "default_name";
            _hp = 10;
            _maxHp = 10;
            _speed = 1.0f;
            _isOnUnitPanel = true;
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