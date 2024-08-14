using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Action.Game
{
    public class PhysicalAttack : Ability
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

