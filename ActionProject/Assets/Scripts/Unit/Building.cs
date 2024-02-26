using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;

namespace Action.Units
{
    public class Building : Unit
    {
        protected PlayerBuildingIdleState _idleState;
        protected PlayerBuildingPrepareState _prepareState;
        protected PlayerBuildingConstructState _constructState;

        protected float _activeDistance;

        public override void Initialize()
        {
            base.Initialize();
            _activeDistance = Constant.INGAMEUI_VISIBLE_DISTANT;
        }

        public virtual void Interact()
        {
            if (StateMachine.CurState == _idleState)
                _StartConstruct();

            //Logger.Log("Building Activate");
        }

        protected bool _CheckPlayerUnitDistance()
        {
            float dist = Vector3.Distance(GameManager.Instance.PlayerUnitObj.transform.position, transform.position);

            if (dist < _activeDistance)
                return true;
            else
                return false;
        }

        void _StartConstruct()
        {
            StateMachine.ChangeState(_prepareState);
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _idleState = new PlayerBuildingIdleState(this);
            _prepareState = new PlayerBuildingPrepareState(this);
            _constructState = new PlayerBuildingConstructState(this);
            base.StateMachine.Initialize(_idleState);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }

}