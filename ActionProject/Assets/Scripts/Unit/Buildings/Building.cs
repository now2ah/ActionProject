using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;
using Action.UI;
using Action.Util;

namespace Action.Units
{
    public class Building : Unit
    {
        protected GameObject _building;
        protected ActionTime _buildingTimer;

        protected GameObject _controlPanel;
        ControlUI _controlUI;
        protected GameObject _buildButton;
        BuildButtonUI _buildButtonUI;
        protected GameObject _requirePanel;
        RequireTextUI _requireTextUI;


        protected PlayerBuildingIdleState _idleState;
        protected PlayerBuildingPrepareState _prepareState;
        protected PlayerBuildingDoneState _doneState;
        protected PlayerBuildingCollapseState _collapseState;

        protected float _activeDistance;
        protected float _constructTime;
        protected int _requireGold;

        Vector3 _indicatorPos;

        public ControlUI ControlUI => _controlUI;
        public BuildButtonUI BuildButtonUI => _buildButtonUI;
        public RequireTextUI RequireTextUI => _requireTextUI;
        public float ActiveDistance => _activeDistance;
        public float ConstructTime => _constructTime;
        public int RequireGold => _requireGold;

        public override void Initialize()
        {
            base.Initialize();

            _building = transform.GetChild(0).gameObject;
            _controlPanel = UIManager.Instance.CreateUI("ControlPanel", UIManager.Instance.InGameCanvas);
            _controlUI = _controlPanel.GetComponent<ControlUI>();
            _controlUI.Initialize(this.gameObject);
            _controlUI.Hide();
            _buildButton = UIManager.Instance.CreateUI("BuildButton", UIManager.Instance.InGameCanvas);
            _buildButtonUI = _buildButton.GetComponent<BuildButtonUI>();
            _buildButtonUI.Initialize();
            _buildButtonUI.SetParent(_controlPanel.transform);
            _buildButtonUI.Hide();
            _requirePanel = UIManager.Instance.CreateUI("RequireText", UIManager.Instance.InGameCanvas);
            _requireTextUI = _requirePanel.GetComponent<RequireTextUI>();
            _requireTextUI.SetParent(_controlPanel.transform);
            _requireTextUI.Initialize();
            _requireTextUI.Hide();
            _idleState = new PlayerBuildingIdleState(this);
            _prepareState = new PlayerBuildingPrepareState(this);
            _doneState = new PlayerBuildingDoneState(this);

            base.StateMachine.Initialize(_idleState);
            _activeDistance = GameManager.Instance.Constants.INGAMEUI_VISIBLE_DISTANT;
            _indicatorPos = transform.position + new Vector3(0, GetComponent<Collider>().bounds.size.y, 0);
        }

        public virtual void Interact()
        {
            if (StateMachine.CurState == _idleState)
                _StartConstruct();
        }

        public void SetVisibleBuilding(bool isOn)
        {
            _building.SetActive(isOn);
        }

        public void StartConstructTimer()
        {
            _buildingTimer = gameObject.AddComponent<ActionTime>();
            
            GameObject timerObj = UIManager.Instance.CreateUI("TimerPanel", UIManager.Instance.InGameCanvas);
            TimerUI timerUI = timerObj.GetComponent<TimerUI>();
            timerUI.Initialize(gameObject);
            timerUI.ActionTime = _buildingTimer;
            _buildingTimer.TickStart(_constructTime);
        }

        void _CheckCommander()
        {
            if (_IsNearPlayerUnit())
            {
                if (StateMachine.CurState == _idleState)
                {
                    _buildButtonUI?.Show();
                    _requireTextUI?.Show();
                }

                if (_buildButtonUI.isShow)
                    _controlUI?.Show();

                GameManager.Instance.CommanderUnit.InteractingBuilding = this.gameObject;
                GameObject indicator = GameManager.Instance.CommanderUnit.Indicator;
                indicator.transform.SetParent(transform);
                indicator.transform.position = _indicatorPos;
                indicator.SetActive(true);
            }
            else
            {
                _controlUI.Hide();
                _buildButtonUI?.Hide();
                GameObject indicator = GameManager.Instance.CommanderUnit.Indicator;
                //indicator?.SetActive(false);
            }
        }

        void _StartConstruct()
        {
            StateMachine.ChangeState(_prepareState);
            StartCoroutine(StartConstructCoroutine());
        }

        IEnumerator StartConstructCoroutine()
        {
            yield return new WaitForSeconds(_constructTime);
            StateMachine.ChangeState(_doneState);
            _indicatorPos = transform.position + new Vector3(0, GetComponent<Collider>().bounds.size.y, 0);
        }

        protected override void Awake()
        {
            base.Awake();
            IsOnUnitPanel = false;
            _constructTime = 0.0f;  //default
            _requireGold = 1;   //default
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            _CheckCommander();
        }
    }

}