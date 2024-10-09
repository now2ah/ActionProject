using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;
using Action.Manager;

namespace Action.Units
{
    public class Barrack : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            Manager.GameManager.Instance.GameData.barrack = UnitData as BuildingData;
            if (UnitData is BuildingData)
                RequireTextUI.Text.text = ((BuildingData)UnitData).requireGold.ToString();
            SetNameUI(UnitData.name);
        }

        void _SetUnitData()
        {
            if (null != GameManager.Instance.GameData.barrack)
            {
                if (UnitData is BuildingData)
                {
                    UnitData.name = GameManager.Instance.GameData.barrack.name;
                    UnitData.hp = GameManager.Instance.GameData.barrack.hp;
                    UnitData.maxHp = GameManager.Instance.GameData.barrack.maxHp;
                    ((BuildingData)UnitData).isBuilt = GameManager.Instance.GameData.barrack.isBuilt;
                    ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.barrack.requireGold;
                    ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.barrack.constructTime;
                    ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.barrack.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.barrack.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.barrack.attackDistance;
                }
                if (GameManager.Instance.GameData.barrack.isBuilt)
                    StateMachine.ChangeState(_doneState);
            }
            else
            {
                UnitData.name = _unitStats.unitName;
                UnitData.hp = _unitStats.maxHp;
                UnitData.maxHp = _unitStats.maxHp;
                ((BuildingData)UnitData).isBuilt = false;
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
            _unitStats = Resources.Load("ScriptableObject/UnitStats/BarrackStats") as UnitStatsSO;
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
        }
    }
}

