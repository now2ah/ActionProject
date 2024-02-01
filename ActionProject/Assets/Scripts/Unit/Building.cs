using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.State;

namespace Action.Units
{
    public class Building : Unit
    {
        PlayerBuildingIdleState _idleState;

        protected float _activeDistance;
        protected bool _isBuilt;

        public virtual void Activate()
        {
            //Logger.Log("Building Activate");
        }

        public virtual void Initialize()
        {
            Logger.Log("Building Init");
            _activeDistance = 10.0f;
            _isBuilt = false;
        }


        protected bool _CheckPlayerUnitDistance()
        {
            float dist = Vector3.Distance(GameManager.Instance.PlayerUnitObj.transform.position, transform.position);

            if (dist < _activeDistance)
                return true;
            else
                return false;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _idleState = new PlayerBuildingIdleState(this);
            base.StateMachine.Initialize(_idleState);
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }

}