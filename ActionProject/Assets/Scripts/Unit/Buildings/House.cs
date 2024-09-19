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
            RequireTextUI.Text.text = BuildingData.requireGold.ToString();
            GameManager.Instance.OnFinishLevel.AddListener(_GenerateGold);
        }

        void _SetUnitData()
        {
            UnitData.name = _unitStats.unitName;
            UnitData.hp = _unitStats.maxHp;
            UnitData.maxHp = _unitStats.maxHp;
            BuildingData.requireGold = _unitStats.requireGold;
            BuildingData.generateGold = _unitStats.generateGold;
            BuildingData.constructTime = _unitStats.constructTime;
        }

        void _GenerateGold()
        {
            GameManager.Instance.GameData.resource.Resources[(int)Game.eResource.GOLD] += BuildingData.generateGold;
            _CreateFloatingUI(BuildingData.generateGold);
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