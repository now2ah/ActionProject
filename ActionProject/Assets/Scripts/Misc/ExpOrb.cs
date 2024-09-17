using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Game
{
    public class ExpOrb : MonoBehaviour, IPoolable<ExpOrb>
    {
        int _expAmount;

        public int ExpAmount { get { return _expAmount; } set { _expAmount = value; } }

        public int PoolID { get; set; }
        public ObjectPooler<ExpOrb> Pool { get; set; }

        protected void OnCollisionEnter(Collision col)
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

