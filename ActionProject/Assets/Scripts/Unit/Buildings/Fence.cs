using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;

namespace Action.Units
{
    public class Fence : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            //if (_isActive)
            //    return;

            base.Initialize();
            _SetUnitData();
            RequireTextUI.Text.text = BuildingData.requireGold.ToString();
        }

        void _SetUnitData()
        {
            UnitData.name = _unitStats.unitName;
            UnitData.hp = _unitStats.maxHp;
            UnitData.maxHp = _unitStats.maxHp;
            BuildingData.requireGold = _unitStats.requireGold;
            BuildingData.constructTime = _unitStats.constructTime;
            BuildingData.attackDamage = _unitStats.attackDamage;
            BuildingData.attackSpeed = _unitStats.attackSpeed;
            BuildingData.attackDistance = _unitStats.attackDistance;
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/FenceStats") as UnitStatsSO;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            //Initialize();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    }
}


