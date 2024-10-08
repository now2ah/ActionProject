using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.SO;
using Action.Manager;

namespace Action.Game
{
    public class AreaAttack : AutoAttackAbility
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
        }

        public override void LevelUp(int level)
        {
            base.LevelUp(level);
            if (0 < level)
            {
                switch (level)
                {
                    case 1:
                        abilityData.attackDamage += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        abilityData.attackDamage += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        abilityData.attackDamage += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        abilityData.attackDamage += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        abilityData.attackDamage += _abilityItem.upgradeInteger[5];
                        break;
                    default:
                        break;
                }
            }
        }

        protected override void _AutoAttack()
        {
            base._AutoAttack();
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/AreaAttackAbility") as AbilityItemSO;
            IsAutoAttack = true;
        }
    }
}

