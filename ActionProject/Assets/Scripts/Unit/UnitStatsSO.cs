using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "UnitStats", menuName = "ScriptableObject/UnitStatsSO", order = 1)]
    public class UnitStatsSO : ScriptableObject
    {
        public string unitName;
        public int maxHp;
        public float speed;
        public float constructTime;
        public int attackDamage;
        public float attackSpeed;
        public float attackDistance;
        public int expAmount;
    }

}