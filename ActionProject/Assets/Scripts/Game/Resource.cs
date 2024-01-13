using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action.Game
{
    public class Resource
    {
        int _food;
        public int Food { get { return _food; } set { _food = value; } }
        int _wood;
        public int Wood { get { return _wood; } set { _wood = value; } }
        int _rock;
        public int Rock { get { return _rock; } set { _rock = value; } }


    }
}