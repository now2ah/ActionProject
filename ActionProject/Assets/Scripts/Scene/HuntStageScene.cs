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
            _stageSystem._floor = _floor;
            _stageSystem.Initialize(Manager.GameManager.Instance.CommanderUnit);
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
