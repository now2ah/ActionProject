using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;
using Action.UI;

namespace Action.Units
{
    public class Building : Unit
    {
        protected GameObject _controlPanel;
        ControlUI _controlUI;

        protected PlayerBuildingIdleState _idleState;
        protected PlayerBuildingPrepareState _prepareState;
        protected PlayerBuildingConstructState _constructState;

        protected float _activeDistance;

        public override void Initialize()
        {
            base.Initialize();
            _controlPanel = UIManager.Instance.CreateUI("ControlPanel", UIManager.Instance.InGameCanvas);
            _controlUI = _controlPanel.GetComponent<ControlUI>();
            _controlUI.Initialize(this.gameObject);
            _controlUI.Hide();
            _idleState = new PlayerBuildingIdleState(this);
            _prepareState = new PlayerBuildingPrepareState(this);
            _constructState = new PlayerBuildingConstructState(this);
            base.StateMachine.Initialize(_idleState);
            _activeDistance = Constant.INGAMEUI_VISIBLE_DISTANT;
        }

        public virtual void Interact()
        {
            if (StateMachine.CurState == _idleState)
                _StartConstruct();

            //Logger.Log("Building Activate");
        }

        void _StartConstruct()
        {
            StateMachine.ChangeState(_prepareState);
        }

        void _VisualizeControlPanel()
        {
            if (_IsNearPlayerUnit())
                _controlUI.Show();
            else
                _controlUI.Hide();
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
            _VisualizeControlPanel();
        }
    }

}