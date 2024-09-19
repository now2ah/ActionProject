using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;
using Action.Manager;


namespace Action.Scene
{
    public class HuntStageScene : MonoBehaviour
    {
        StageSystem _stageSystem;
        public GameObject _floor;
        ActionTime _startTimer;
        
        bool _isStart;

        public void Initialize()
        {
            //GameManager.Instance.ChangePhase(eGamePhase.Hunt);
            _stageSystem.Initialize(Manager.GameManager.Instance.CommanderUnit);
            Manager.CameraManager.Instance.CreateFixedVirtualCamera();
        }

        public void WaveStart()
        {
            Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 1.5f);
        }

        private void Awake()
        {
            _stageSystem = gameObject.AddComponent<StageSystem>();
            _startTimer = gameObject.AddComponent<ActionTime>();
            _isStart = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            _startTimer.TickStart(1.0f);
            //temp
            Manager.GameManager.Instance.CommanderUnit.ActivateAutoAttack(0);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Manager.GameManager.Instance.IsLive)
                return;

            if (_startTimer.IsFinished && !_isStart)
            {
                _isStart = true;
                WaveStart();
            }
        }
    }
}
