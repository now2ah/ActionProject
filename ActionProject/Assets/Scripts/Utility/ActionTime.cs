using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Util
{
    public class ActionTime : MonoBehaviour
    {
        Time _time;
        Time _endTime;
        string _timeString;

        public ActionTime(Time endTime, string timeString)
        {
            _endTime = endTime;
            _timeString = timeString;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
