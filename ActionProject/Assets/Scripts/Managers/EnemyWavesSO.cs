using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.SO
{
    [CreateAssetMenu(fileName = "EnemyWaves", menuName = "ScriptableObject/EnemyWavesSO", order = 2)]
    public class EnemyWavesSO : ScriptableObject
    {
        public int integer;
        public List<ListClass> listClass;
    }
}