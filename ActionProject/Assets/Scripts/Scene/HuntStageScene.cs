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

        public void Initialize()
        {
            //_stageSystem.Ground = Resources.Load("Prefabs/Misc/HuntStageGround") as GameObject;
            _stageSystem.Initialize(Manager.GameManager.Instance.CommanderUnit);
            Manager.CameraManager.Instance.CreateFixedVirtualCamera();
            Manager.GameManager.Instance.AddAllEnemySpawners();
            Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 1.0f, 1, true);
            Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 1.0f, 2, true);
            Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 1.0f, 3, true);
            Manager.GameManager.Instance.StartWave(Manager.GameManager.Instance.HuntEnemyWaves, 1.0f, 4, true);
        }

        private void Awake()
        {
            _stageSystem = gameObject.AddComponent<StageSystem>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
