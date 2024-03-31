using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "UnitStat", menuName = "ScriptableObject/UnitStatSO", order = 1)]
    public class UnitStatSO : ScriptableObject
    {
        string unitName;
        int maxHp;
        float speed;
        float constructTime;
        int attackDamage;
        float attackSpeed;
        float attackDistance;
    }

}