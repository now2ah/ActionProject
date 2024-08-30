using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;

namespace Action.Game
{
    public class HuntSpawner : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (null != GameManager.Instance.CommanderUnit)
                transform.position = GameManager.Instance.CommanderUnit.transform.position;
        }
    }
}

