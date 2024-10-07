using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;

namespace Action.Units
{
    public class Fence : Building
    {
        UnitStatsSO _unitStats;
        GameObject _gate;

        public override void Initialize()
        {
            //if (_isActive)
            //    return;

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
            BuildingData.attackDamage = _unitStats.attackDamage;
            BuildingData.attackSpeed = _unitStats.attackSpeed;
            BuildingData.attackDistance = _unitStats.attackDistance;
        }

        void _ControlGate(bool isOn)
        {
            _gate.SetActive(isOn);
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/FenceStats") as UnitStatsSO;
            _gate = transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
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
            if (BuildingData.isBuilt && _IsNearPlayerUnit())
                _ControlGate(true);
            else
                _ControlGate(false);
        }
    }
}


