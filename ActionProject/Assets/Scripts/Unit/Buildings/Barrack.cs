using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;

namespace Action.Units
{
    public class Barrack : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            Manager.GameManager.Instance.SetBuildingData(this.name);
            RequireTextUI.Text.text = BuildingData.requireGold.ToString();
        }

        void _SetUnitData()
        {
            BuildingData.name = _unitStats.unitName;
            BuildingData.hp = _unitStats.maxHp;
            BuildingData.maxHp = _unitStats.maxHp;
            BuildingData.requireGold = _unitStats.requireGold;
            BuildingData.constructTime = _unitStats.constructTime;
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

