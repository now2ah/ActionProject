using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Manager;
using Action.SO;

namespace Action.Game
{
    public class DirectionalAttack : AutoAttackAbility
    {
        AbilityItemSO _abilityItem;

        void _SetAbilityData()
        {
            abilityData.type = Enums.eAbility.DIRECTIONAL;
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
            if (null != AttackTimer)
            {
                if (!AttackTimer.IsStarted)
                {
                    //attack logic
                    _CreateProjectile(abilityData.attackDamage);
                    AttackTimer.TickStart(abilityData.attackSpeed);
                    AudioManager.Instance.PlaySFX(AudioManager.eSfx.FIRE);
                }

                if (AttackTimer.IsFinished)
                    AttackTimer.ResetTimer();
            }
        }

        void _CreateProjectile(float attackDamage)
        {
            Vector3 shootPosition = transform.position + transform.forward * 2.0f + transform.up * 1.2f;
            //shootPosition.y = GameManager.Instance.Constants.HUNT_PROJECTILE_Y_POS;
            NormalProjectile projectile = (NormalProjectile)PoolManager.Instance.NormalProjectilePool.GetNew();
            projectile.transform.position = shootPosition;
            projectile.transform.rotation = transform.rotation;
            if (gameObject.TryGetComponent<Unit>(out Unit comp))
                projectile.Initialize(comp, attackDamage);
        }

        protected override void Awake()
        {
            base.Awake();
            _abilityItem = Resources.Load("ScriptableObject/Abilities/DirectionalAttackAbility") as AbilityItemSO;
            IsAutoAttack = true;
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

