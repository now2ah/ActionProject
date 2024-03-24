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
        protected GameObject _foundationPanel;
        FoundationUI _foundationUI;

        protected PlayerBuildingIdleState _idleState;
        protected PlayerBuildingPrepareState _prepareState;
        protected PlayerBuildingDoneState _doneState;
        protected PlayerBuildingCollapseState _collapseState;

        protected float _activeDistance;
        protected float _constructTime;

        public ControlUI ControlUI => _controlUI;
        public FoundationUI FoundationUI => _foundationUI;
        public float ActiveDistance => _activeDistance;

        public override void Initialize()
        {
            base.Initialize();
            IsOnUnitPanel = false;

            _building = transform.GetChild(0).gameObject;
            _controlPanel = UIManager.Instance.CreateUI("ControlPanel", UIManager.Instance.InGameCanvas);
            _controlUI = _controlPanel.GetComponent<ControlUI>();
            _controlUI.Initialize(this.gameObject);
            _controlUI.Hide();
            _foundationPanel = UIManager.Instance.CreateUI("FoundationPanel", UIManager.Instance.InGameCanvas);
            _foundationUI = _foundationPanel.GetComponent<FoundationUI>();
            _foundationUI.Initialize(this.gameObject);
            _foundationUI.SetParent(_controlPanel.transform);
            _foundationUI.Hide();
            _constructTime = 5.0f;  //default
            _idleState = new PlayerBuildingIdleState(this);
            _prepareState = new PlayerBuildingPrepareState(this);
            _doneState = new PlayerBuildingDoneState(this);

            base.StateMachine.Initialize(_idleState);
            _activeDistance = Constant.INGAMEUI_VISIBLE_DISTANT;
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
                _controlUI?.Show();
                if (StateMachine.CurState == _idleState)
                {
                    _foundationUI?.Show();
                }
                GameManager.Instance.PlayerUnit.InteractingBuilding = this.gameObject;
            }
            else
            {
                _controlUI.Hide();
                _foundationUI?.Hide();
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
        }

        protected override void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            _CheckCommander();
        }
    }

}