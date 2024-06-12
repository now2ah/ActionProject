using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [System.Serializable]
    public class EnemyWave
    {
        public List<EnemyGroup> enemyGroupList;
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public Enums.eEnemyType type;
        public int enemyAmount;
    }

    [CreateAssetMenu(fileName = "EnemyWaves", menuName = "ScriptableObject/EnemyWavesSO", order = 2)]
    public class EnemyWaves : ScriptableObject
    {
        public List<EnemyWave> enemyWaveList;
    }
}