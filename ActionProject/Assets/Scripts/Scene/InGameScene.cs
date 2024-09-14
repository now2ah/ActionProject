using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Manager;
using Action.Util;


namespace Action.Scene
{
    public class InGameScene : MonoBehaviour
    {
        ActionTime _startTimer;
        bool _isWaveStart;

        private void Awake()
        {
            _startTimer = gameObject.AddComponent<ActionTime>();
        }

        // Start is called before the first frame update
        void Start()
        {

            
            //GameManager.Instance.ChangePhase(eGamePhase.Defense);
        }

        private void Update()
        {
        }
    }
}
