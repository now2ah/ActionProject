using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.SO;
using Action.Util;

namespace Action.Game
{
    public class PhysicalAttack : Ability
    {
        AbilityItemSO _abilityItem;
        ActionTime _timer;
        float _coolTime;

        public ActionTime Timer => _timer;
        public float CoolTime => _coolTime;

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
            _abilityItem = Resources.Load("ScriptableObject/Abilities/PhysicalAttackAbility") as AbilityItemSO;
            _timer = gameObject.AddComponent<ActionTime>();
            _coolTime = 1.5f;
            AbilityName = _abilityItem.abilityName;
            Description = _abilityItem.abilityDescription;
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

