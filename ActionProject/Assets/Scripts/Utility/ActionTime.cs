using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public class ActionTime : MonoBehaviour
    {
        float _time;
        float _startTime;
        float _endTime;
        string _timeString;
        bool _isStarted;
        bool _isFinished;
        public bool IsStarted => _isStarted;
        public bool IsFinished => _isFinished;

        public void TickStart()
        {
            _isStarted = true;
            _isFinished = false;
            _startTime = Time.time;
        }

        public void TickStart(float endTime)
        {
            _isStarted = true;
            _isFinished = false;
            _startTime = Time.time;
            _endTime = endTime;
        }

        public float GetTimeFloat()
        {
            return _time;
        }

        public string GetTimeString()
        {
            float time = Mathf.Floor(_time * 10.0f) / 10.0f;
            _timeString = time.ToString();
            return _timeString;
        }

        public void ResetTimer()
        {
            _time = Time.time;
            _isStarted = false;
        }

        public void SetEndTime(float endTime)
        {
            _endTime = endTime;
        }

        void _Tick()
        {
            _time = Time.time - _startTime;

            if (_time > _endTime)
            {
                _isStarted = false;
                _isFinished = true;
            }
        }

        private void Awake()
        {
            _time = 0;
            _startTime = 0;
            _endTime = 0;
            _timeString = "default time string";
            _isStarted = false;
            _isFinished = false;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            if (!Manager.GameManager.Instance.IsLive)
                return;

            if (_isStarted)
                _Tick();
        }
    }
}
