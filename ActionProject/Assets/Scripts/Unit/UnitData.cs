using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Units
{
    [System.Serializable]
    public class UnitData
    {
        public string name;
        public float hp;
        public float maxHp;
        public float growthHp;
    }

    [System.Serializable]
    public class PlayerUnitData : UnitData
    {
        public float speed;
        public float attackDamage;
        public float growthAttackDamage;
        public int level;
        public int exp;
        public int nextExp;
    }

    [System.Serializable]
    public class BuildingData : UnitData
    {
        public bool isBuilt;
    }

    [System.Serializable]
    public class EnemyUnitData : UnitData
    {
        public float speed;
        public float attackDamage;
        public float attackSpeed;
        public float attackDistance;
        public int expAmount;
    }
}

