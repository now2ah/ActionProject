using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Resource
    {
        int _gold = 0;
        public int Gold { get { return _gold; } set { _gold = value; } }
        int _food = 0;
        public int Food { get { return _food; } set { _food = value; } }
        int _wood = 0;
        public int Wood { get { return _wood; } set { _wood = value; } }
        int _iron = 0;
        public int Iron { get { return _iron; } set { _iron = value; } }
    }
}