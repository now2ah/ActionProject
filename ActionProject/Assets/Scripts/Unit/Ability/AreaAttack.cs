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
        float _attackDistance;

        public float AttackDistance { get { return _attackDistance; } set { _attackDistance = value; } }

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
                        AttackDamage += _abilityItem.upgradeInteger[1];
                        break;
                    case 2:
                        AttackDamage += _abilityItem.upgradeInteger[2];
                        break;
                    case 3:
                        AttackDamage += _abilityItem.upgradeInteger[3];
                        break;
                    case 4:
                        AttackDamage += _abilityItem.upgradeInteger[4];
                        break;
                    case 5:
                        AttackDamage += _abilityItem.upgradeInteger[5];
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
            AbilityName = _abilityItem.abilityName;
            Description = _abilityItem.abilityDescription;
            //default
            AttackPeriod = 1.0f;
            AttackDamage = 5.0f;
        }
    }
}

