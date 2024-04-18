using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Units
{
    public partial class Unit : MonoBehaviour
    {
        public struct DamageMessage
        {
            public Unit damager;
            public int amount;
        }
    }
}
