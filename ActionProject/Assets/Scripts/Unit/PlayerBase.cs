using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.SO;

namespace Action.Units
{
    public class PlayerBase : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            if (_isActive)
                return;

            base.Initialize();

            StateMachine.ChangeState(_prepareState);
            StateMachine.ChangeState(_doneState);
            BuildingData.isBuilt = true;
        }

        void SetUnitData()
        {
            
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/PlayerBaseStats") as UnitStatsSO;
            _constructTime = 0.0f;
            UnitData.maxHp = _unitStats.maxHp;
            UnitData.hp = _unitStats.maxHp;
            UnitData.name = _unitStats.name;
        }

        protected override void Start()
        {
            base.Start();
            //Initialize();
        }

        protected override void Update()
        {
            base.Update();
        }
    }

}
