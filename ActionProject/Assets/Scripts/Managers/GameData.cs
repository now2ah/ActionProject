using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Units;
using Action.Game;

namespace Action.Manager
{
    [System.Serializable]
    public class GameData
    {
        public int curHuntWaveOrder;
        public BuildingData towerBaseN;
        public BuildingData towerBaseS;
        public BuildingData fence;
        public Resource resource;
    }
}

