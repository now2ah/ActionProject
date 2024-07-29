using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;


namespace Action.Game
{
    public class HuntStageGround : MonoBehaviour, IPoolable<HuntStageGround>
    {
        public int PoolID { get; set; }
        public ObjectPooler<HuntStageGround> Pool { get; set; }
    }
}
