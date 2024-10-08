using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.SO;

namespace Action.Game
{
    public class DamageUpAbility : Ability
    {
        AbilityItemSO _abilityItem;

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
        }

        public override void Activate(bool isOn)
        {
            base.Activate(isOn);
            Commander = GameManager.Instance.CommanderUnit;
        }

        public override void LevelUp(int level)
        {
            base.LevelUp(level);
            if (0 < level)
            {
                switch (level)
                {
                    case 1:
                        Commander.PlayerUnitData.attackDamage += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        Commander.PlayerUnitData.attackDamage += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        Commander.PlayerUnitData.attackDamage += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        Commander.PlayerUnitData.attackDamage += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        Commander.PlayerUnitData.attackDamage += _abilityItem.upgradeInteger[5];
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/DamageUpAbility") as AbilityItemSO;
            IsAutoAttack = false;
        }
    }

    public class HPUpAbility : Ability
    {
        AbilityItemSO _abilityItem;

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
        }

        public override void Activate(bool isOn)
        {
            base.Activate(isOn);
            Commander = GameManager.Instance.CommanderUnit;
        }

        public override void LevelUp(int level)
        {
            base.LevelUp(level);
            if (0 < level)
            {
                switch (level)
                {
                    case 1:
                        Commander.UnitData.maxHp += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        Commander.UnitData.maxHp += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        Commander.UnitData.maxHp += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        Commander.UnitData.maxHp += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        Commander.UnitData.maxHp += _abilityItem.upgradeInteger[5];
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/HPUpAbility") as AbilityItemSO;
            IsAutoAttack = false;
        }
    }

    public class SpeedUpAbility : Ability
    {
        AbilityItemSO _abilityItem;

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
        }

        public override void LevelUp(int level)
        {
            base.LevelUp(level);
            if (1 < level)
            {
                switch (level)
                {
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/DamageUpAbility") as AbilityItemSO;
            IsAutoAttack = false;
        }
    }
}

