using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    [System.Serializable]
    public class AbilityData
    {
        public Enums.eAbility type;
        public bool isActivated;
        public int level;
        public string abilityName;
        public string description;
        public float attackDamage;
        public float attackSpeed;
        public float attackDistance;
    }
}

