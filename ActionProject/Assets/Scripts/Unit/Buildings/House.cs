using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Action.Manager;
using Action.UI;
using Action.SO;

namespace Action.Units
{
    public class House : Building
    {
        UnitStatsSO _unitStats;

        public override void Initialize()
        {
            base.Initialize();
            _SetUnitData();
            UnitPanel.ApplyHPValue(UnitData.hp, UnitData.maxHp);
            Manager.GameManager.Instance.GameData.house = UnitData as BuildingData;
            if (UnitData is BuildingData)
                RequireTextUI.Text.text = ((BuildingData)UnitData).requireGold.ToString();
            SetNameUI(UnitData.name);
            GameManager.Instance.OnFinishLevel.AddListener(_GenerateGold);
        }

        void _SetUnitData()
        {
            if (null != GameManager.Instance.GameData.house)
            {
                if (UnitData is BuildingData)
                {
                    UnitData.name = GameManager.Instance.GameData.house.name;
                    UnitData.hp = GameManager.Instance.GameData.house.hp;
                    UnitData.maxHp = GameManager.Instance.GameData.house.maxHp;
                    ((BuildingData)UnitData).isBuilt = GameManager.Instance.GameData.house.isBuilt;
                    ((BuildingData)UnitData).requireGold = GameManager.Instance.GameData.house.requireGold;
                    ((BuildingData)UnitData).constructTime = GameManager.Instance.GameData.house.constructTime;
                    ((BuildingData)UnitData).attackDamage = GameManager.Instance.GameData.house.attackDamage;
                    ((BuildingData)UnitData).attackSpeed = GameManager.Instance.GameData.house.attackSpeed;
                    ((BuildingData)UnitData).attackDistance = GameManager.Instance.GameData.house.attackDistance;
                }
                if (GameManager.Instance.GameData.house.isBuilt)
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

        void _GenerateGold()
        {
            GameManager.Instance.GameData.resource.Gold += ((BuildingData)UnitData).generateGold;
            _CreateFloatingUI(((BuildingData)UnitData).generateGold);
        }

        void _CreateFloatingUI(int gold)
        {
            GameObject floatingUI = UIManager.Instance.CreateUI("FloatingPanel", UIManager.Instance.InGameCanvas);
            floatingUI.transform.position = CameraManager.Instance.MainCamera.Camera.WorldToScreenPoint(transform.position);
            if (floatingUI.TryGetComponent<UI.FloatingPanelUI>(out UI.FloatingPanelUI comp))
            {
                comp.Initialize(gameObject);
                comp.Text.text = "+" + gold.ToString();
                comp.Text.color = Color.yellow;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _unitStats = Resources.Load("ScriptableObject/UnitStats/HouseStats") as UnitStatsSO;
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