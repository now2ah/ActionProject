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
        bool _isStart;

        public ActionTime(float endTime)
        {
            _startTime = Time.time;
            _endTime = _startTime + endTime;
        }

        public void TickStart(float endTime)
        {
            _isStart = true;
            _startTime = Time.time;
            _endTime = endTime;
        }

        public string GetTimeString()
        {
            float time = Mathf.Floor(_time * 10.0f) / 10.0f;
            _timeString = time.ToString();
            return _timeString;
        }

        void _Tick()
        {
            _time = Time.time - _startTime;

            if (_time > _endTime)
                _isStart = false;
                
        }

        private void Awake()
        {
            _time = 0;
            _startTime = 0;
            _endTime = 0;
            _timeString = "default time string";
            _isStart = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (_isStart)
                _Tick();
        }
    }
}
