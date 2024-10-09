using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;
using Action.Manager;

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
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            Manager.GameManager.Instance.GameData.fence = UnitData as BuildingData;
            if (UnitData is BuildingData)
                RequireTextUI.Text.text = ((BuildingData)UnitData).requireGold.ToString();
            UnitPanel.PanelPosition = UI.ePanelPosition.CENTER;
            ControlUI.PanelPosition = UI.ePanelPosition.CENTER;
            SetNameUI(UnitData.name);
        }

        void _SetUnitData()
        {
            if (null != GameManager.Instance.GameData.fence)
            {
                if (UnitData is BuildingData)
                {
                    UnitData.name = GameManager.Instance.GameData.fence.name;
                    UnitData.hp = GameManager.Instance.GameData.fence.hp;
                    UnitData.maxHp = GameManager.Instance.GameData.fence.maxHp;
                    ((BuildingData)UnitData).isBuilt = GameManager.Instance.GameData.fence.isBuilt;
                    ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.fence.requireGold;
                    ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.fence.constructTime;
                    ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.fence.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.fence.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.fence.attackDistance;
                }
                if (GameManager.Instance.GameData.fence.isBuilt)
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
            if (UnitData is BuildingData)
            {
                if (((BuildingData)UnitData).isBuilt && _IsNearPlayerUnit())
                    _ControlGate(false);
                else
                    _ControlGate(true);
            }
        }
    }
}


