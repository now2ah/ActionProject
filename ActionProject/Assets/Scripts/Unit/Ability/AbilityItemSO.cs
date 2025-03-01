using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "AbilityItem", menuName = "ScriptableObject/AbilityItemSO", order = 3)]
    public class AbilityItemSO : ScriptableObject
    {
        public string abilityName;
        public string abilityDescription;
        public float attackDamage;
        public float attackSpeed;
        public float attackDistance;
        public int[] upgradeInteger;
    }
}