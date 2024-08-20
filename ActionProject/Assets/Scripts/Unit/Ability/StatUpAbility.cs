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

        public override void Activate()
        {
            base.Activate();
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
                        Commander.AttackDamage += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        Commander.AttackDamage += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        Commander.AttackDamage += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        Commander.AttackDamage += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        Commander.AttackDamage += _abilityItem.upgradeInteger[5];
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
            AbilityName = _abilityItem.abilityName;
            Description = _abilityItem.abilityDescription;
        }
    }

    public class HPUpAbility : Ability
    {
        AbilityItemSO _abilityItem;

        public override void Activate()
        {
            base.Activate();
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
                        Commander.MaxHp += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        Commander.MaxHp += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        Commander.MaxHp += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        Commander.MaxHp += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        Commander.MaxHp += _abilityItem.upgradeInteger[5];
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
            AbilityName = _abilityItem.abilityName;
            Description = _abilityItem.abilityDescription;
        }
    }

    public class SpeedUpAbility : Ability
    {
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
            AbilityName = "SpeedUp";
            Description = "Increase Commander's speed";
        }
    }
}

