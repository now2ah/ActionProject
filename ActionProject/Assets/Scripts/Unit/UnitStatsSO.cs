using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "UnitStats", menuName = "ScriptableObject/UnitStatsSO", order = 1)]
    public class UnitStatsSO : ScriptableObject
    {
        public string unitName;
        public float maxHp;
        public float growthMaxHp;
        public float speed;
        public int requireGold;
        public int generateGold;
        public float constructTime;
        public float attackDamage;
        public float growthAttackDamage;
        public float attackSpeed;
        public float attackDistance;
        public int expAmount;
        public int goldAmount;
        public int nextExp;
    }

}