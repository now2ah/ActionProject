using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.SO;
using Action.Util;
using Action.Units;

namespace Action.Game
{
    public class PhysicalAttack : Ability
    {
        AbilityItemSO _abilityItem;
        ActionTime _timer;

        public ActionTime Timer => _timer;

        void _SetAbilityData()
        {
            abilityData.isActivated = false;
            abilityData.level = 0;
            abilityData.abilityName = _abilityItem.abilityName;
            abilityData.description = _abilityItem.abilityDescription;
            abilityData.attackDamage = _abilityItem.attackDamage;
            abilityData.attackSpeed = _abilityItem.attackSpeed;
        }

        public override void Initialize()
        {
            base.Initialize();
            _SetAbilityData();
            Commander = GameManager.Instance.CommanderUnit;
        }

        public override void Activate(bool isOn)
        {
            base.Activate(isOn);
            
        }

        public override void LevelUp(int level)
        {
            PlayerUnitData data = Commander.UnitData as PlayerUnitData;
            base.LevelUp(level);
            if (0 < level)
            {
                switch (level)
                {
                    case 1:
                        data.attackDamage += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        data.attackDamage += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        data.attackDamage += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        data.attackDamage += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        data.attackDamage += _abilityItem.upgradeInteger[5];
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/PhysicalAttackAbility") as AbilityItemSO;
            _timer = gameObject.AddComponent<ActionTime>();
            IsAutoAttack = false;
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

