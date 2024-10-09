using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.State;
using Action.SO;
using Action.Manager;

namespace Action.Units
{
    public class PlayerBase : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            //if (_isActive)
            //    return;

            base.Initialize();
            SetUnitData();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            Manager.GameManager.Instance.GameData.playerBase = UnitData as BuildingData;
            StateMachine.ChangeState(_prepareState);
            StateMachine.ChangeState(_doneState);
            if (UnitData is BuildingData)
            {
                ((BuildingData)UnitData).isBuilt = true;
                SetNameUI(UnitData.name);
            }
        }

        void SetUnitData()
        {
            if (null != GameManager.Instance.GameData.playerBase)
            {
                if (UnitData is BuildingData)
                {
                    UnitData.name = GameManager.Instance.GameData.playerBase.name;
                    UnitData.hp = GameManager.Instance.GameData.playerBase.hp;
                    UnitData.maxHp = GameManager.Instance.GameData.playerBase.maxHp;
                    ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.playerBase.requireGold;
                    ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.playerBase.constructTime;
                    ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.playerBase.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.playerBase.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.playerBase.attackDistance;
                }
            }
            else
            {
                UnitData.name = _unitStats.unitName;
                UnitData.hp = _unitStats.maxHp;
                UnitData.maxHp = _unitStats.maxHp;
                ((BuildingData)UnitData).requireGold = _unitStats.requireGold;
                ((BuildingData)UnitData).constructTime = _unitStats.constructTime;
                ((BuildingData)UnitData).attackDamage = _unitStats.attackDamage;
                ((BuildingData)UnitData).attackSpeed = _unitStats.attackSpeed;
                ((BuildingData)UnitData).attackDistance = _unitStats.attackDistance;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/PlayerBaseStats") as UnitStatsSO;
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
