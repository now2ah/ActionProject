using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;


namespace Action.Scene
{
    public class HuntStageScene : MonoBehaviour
    {
        StageSystem _stageSystem;
        public GameObject _floor;
        ActionTime _startTimer;
        
        bool _isStart;

        public GameObject SpawnerObj;

        public void Initialize()
        {
            //_stageSystem.Ground = Resources.Load("Prefabs/Misc/HuntStageGround") as GameObject;
            _stageSystem.Initialize(Manager.GameManager.Instance.CommanderUnit);
            Manager.CameraManager.Instance.CreateFixedVirtualCamera();
            Manager.GameManager.Instance.AddAllEnemySpawners();
            if (null != SpawnerObj)
                SpawnerObj.transform.SetParent(Manager.GameManager.Instance.CommanderObj.transform, false);
        }

        public void WaveStart()
        {
            for(int i = 0; i<Manager.GameManager.Instance.EnemySpawners.Count; i++)
                Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 3.0f, i, true);
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
            _startTimer.TickStart(3.0f);
        }

        // Update is called once per frame
        void Update()
        {
            if (_startTimer.IsFinished && !_isStart)
            {
                _isStart = true;
                WaveStart();
            }
        }
    }
}