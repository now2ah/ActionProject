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
            if (!GameManager.Instance.IsPlaying)
            {
                GameManager.Instance.GameStart();
                CameraManager.Instance.CreateFixedVirtualCamera();
                UIManager.Instance.CreateTownStagePanel();
            }
            else
            {
                CameraManager.Instance.CreateFixedVirtualCamera();
                _startTimer.TickStart(3.0f);
            }
            //GameManager.Instance.ChangePhase(eGamePhase.Defense);
        }

        private void Update()
        {
            if (_startTimer.IsFinished && !_isWaveStart)
            {
                _isWaveStart = true;
                GameManager.Instance.StartWave(GameManager.Instance.EnemyUnits, 1.0f, GameManager.Instance.DefenseSpawner.GetComponent<Spawner>());
            }
        }
    }
}
