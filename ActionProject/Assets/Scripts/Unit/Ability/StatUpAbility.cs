using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class DamageUpAbility : Ability
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
            AbilityName = "DamageUp";
            Description = "Increase Commander's damage";
        }
    }

    public class HPUpAbility : Ability
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
            AbilityName = "HPUp";
            Description = "Increase Commander's HP";
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

