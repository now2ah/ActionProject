using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action.Util;

namespace Action.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        float _time;
        float _startTime;
        bool _isPlaying;

        public override void Initialize()
        {
            base.Initialize();
            _time = 0.0f;
        }

        public void GameStart()
        {
            _isPlaying = true;
            _StartTimer();
        }

        public void GameOver()
        {

        }

        void _StartTimer()
        {
            _startTime = Time.realtimeSinceStartup;
        }
        
        void _CalculateTime()
        {
            _time = Time.realtimeSinceStartup - _startTime;
            Debug.Log(_time);
        }

        void _StartWave()
        {

        }

        private void Update()
        {
            if(_isPlaying)
                _CalculateTime();
        }
    }
}
